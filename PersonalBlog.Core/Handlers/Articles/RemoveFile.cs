using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Exceptions;
using PersonalBlog.Core.Extensions;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Handlers.Articles;

public class RemoveFile
{
    public record Command(PersonalBlogGRpc.RemoveFile File) : IRequest;

    public class Handler(ICurrentUser currentUser, IPbDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var articleId = request.File.ArticleId;
            var fileUrl = request.File.FileUrl;
            
            var article = await GetArticleAsync(articleId, cancellationToken);

            article.MediaUrls.Remove(fileUrl);

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
    }
}