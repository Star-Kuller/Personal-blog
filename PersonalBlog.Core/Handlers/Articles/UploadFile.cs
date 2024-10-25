using Google.Protobuf;
using Grpc.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalBlog.Core.Exceptions;
using PersonalBlog.Core.Extensions;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;
using PersonalBlogGRpc;

namespace PersonalBlog.Core.Handlers.Articles;

public class UploadFile
{
    public record Command(IAsyncStreamReader<FileChunk> FileStream, ServerCallContext context) : IRequest;

    public class Handler(ILogger<Handler> logger, IPbDbContext context, ICurrentUser currentUser)
        : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var stream = request.FileStream;
            var host = request.context.Host;

            await stream.MoveNext();
            var firstChunk = stream.Current;

            var article = await GetArticleAsync(firstChunk.ArticleId, cancellationToken);
            var fileName = firstChunk.FileName;

            var directoryPath = GetDirectory(article.Id);
            var filePath = Path.Combine(directoryPath, fileName);

            var i = 1;
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);
            
            while (File.Exists(filePath))
            {
                fileName = $"{fileNameWithoutExtension}({i}){extension}";
                filePath = Path.Combine(directoryPath, fileName);
                i++;
            }

            await CreateFileFromStream(stream, firstChunk, article.Id, filePath, cancellationToken);

            article.MediaUrls.Add(GetLink(host, article.Id, fileName));

            await context.SaveChangesAsync(cancellationToken);
        }

        private async Task<Article> GetArticleAsync(long articleId, CancellationToken cancellationToken)
        {
            return await context.Articles
                       .Where(x => x.AuthorId == currentUser.Id || currentUser.IsAdmin)
                       .NotDeleted()
                       .WithId(articleId)
                       .FirstOrDefaultAsync(cancellationToken)
                   ?? throw new NotFoundException(nameof(Article), articleId);
        }

        private string GetDirectory(long articleId)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Articles/{articleId}");
            if (Directory.Exists(directoryPath)) return directoryPath;

            Directory.CreateDirectory(directoryPath);
            logger.LogInformation("Created directory: {DirectoryPath}", directoryPath);
            return directoryPath;
        }

        private async Task CreateFileFromStream(
            IAsyncStreamReader<FileChunk> stream,
            FileChunk firstChunk,
            long articleId,
            string filePath,
            CancellationToken cancellationToken)
        {
            try
            {
                var fileName = firstChunk.FileName;
                var totalChunks = firstChunk.TotalChunks;

                logger.LogInformation("Start uploading file {Name} to article {ArticleId}: 1/{Total}",
                    fileName, articleId, totalChunks);

                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ReadStreamToFile(stream, fileStream, firstChunk.File, fileName, totalChunks,
                        cancellationToken);
                }

                logger.LogInformation("Uploaded file {Name} to article {ArticleId}",
                    fileName, articleId);
            }
            catch (NotAllChunksReceivedException)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    logger.LogWarning("Deleted partially uploaded file {Name} due to an error", firstChunk.FileName);
                }
            }
            catch
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
                throw;
            }
        }

        private async Task ReadStreamToFile(
            IAsyncStreamReader<FileChunk> stream,
            FileStream fileStream,
            ByteString firstChunkData,
            string fileName,
            int totalChunks,
            CancellationToken cancellationToken)
        {
            await fileStream.WriteAsync(firstChunkData.ToByteArray(), cancellationToken);

            var i = 1;
            while (await stream.MoveNext())
            {
                i++;
                logger.LogInformation("Uploading file {Name}: {I}/{Total}",
                    fileName, i, totalChunks);
                var chunk = stream.Current.File;
                await fileStream.WriteAsync(chunk.ToByteArray(), cancellationToken);
            }

            if (i < totalChunks)
            {
                throw new NotAllChunksReceivedException(i, totalChunks);
            }
        }

        private string GetLink(string host, long articleId, string fileName)
        {
            var link = $"https://{host}/Articles/{articleId}/{fileName}";
            logger.LogInformation("Created file: {Link}", link);
            return link;
        }
    }
}