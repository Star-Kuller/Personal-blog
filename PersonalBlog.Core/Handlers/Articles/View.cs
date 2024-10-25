using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Exceptions;
using PersonalBlog.Core.Extensions;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;
using PersonalBlogGRpc;
using Comment = PersonalBlogGRpc.Comment;

namespace PersonalBlog.Core.Handlers.Articles;

public class View
{
    public record Query(long Id) : IRequest<ArticleInfo>;

    public class Handler(ICurrentUser currentUser, IPbDbContext context) : IRequestHandler<Query, ArticleInfo>
    {
        public async Task<ArticleInfo> Handle(Query request, CancellationToken cancellationToken)
        {
            var article = await context.Articles
                              .Include(x => x.Comments)
                              .ThenInclude(x => x.Author)
                              .Include(x => x.Likes)
                              .Where(x => x.AuthorId == currentUser.Id || x.IsPublished)
                              .Where(x => !x.IsDeleted || currentUser.IsAdmin)
                              .WithId(request.Id)
                              .FirstOrDefaultAsync(cancellationToken)
                          ?? throw new NotFoundException(nameof(Article));

            return new ArticleInfo
            {
                Id = article.Id,
                Title = article.Name,
                Text = article.Text,
                IsPublished = article.IsPublished,
                Liked = article.Likes.Select(l => l.Id).Contains(currentUser.Id),
                LikesCount = article.Likes.Count,
                CommentsCount = article.Comments.Count,
                AuthorId = article.AuthorId,
                MediaUrls = { article.MediaUrls },
                Comments =
                {
                    article.Comments
                        .Where(c => !c.IsDeleted)
                        .Select(c =>
                            new Comment
                            {
                                AuthorId = c.AuthorId,
                                AuthorName = c.Author?.Name,
                                AuthorAvatar = c.Author?.Base64Avatar,
                                Message = c.Message
                            })
                }
            };
        }
    }
}