using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Extensions;
using PersonalBlog.Core.Interfaces;
using PersonalBlogGRpc;
using PagedListQuery = PersonalBlogGRpc.PagedListQuery;
using PagedListQueryBase = PersonalBlog.Core.Common.PagedListQuery;

namespace PersonalBlog.Core.Handlers.Articles;

public class PagedList
{
    public record Query(PagedListQuery Form) : PagedListQueryBase(Form.Page, Form.Size), IRequest<PagedListResult>;

    public class Handler(ICurrentUser currentUser, IPbDbContext context) : IRequestHandler<Query, PagedListResult>
    {
        public async Task<PagedListResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var items = await context.Articles
                .Include(x => x.Comments)
                .Include(x => x.Likes)
                .Where(x => x.IsPublished || x.AuthorId == currentUser.Id)
                .NotDeleted()
                .OrderByDescending(x => x.CreatedAt)
                .Count(out var rowsCount)
                .SkipItemsAsync(request, cancellationToken);
                
            var mappedItems = items
                .Select(article => new ArticleShort
                {
                    Id = article.Id,
                    Title = article.Name,
                    Text = article.Text.Length > 100
                        ? new string(article.Text.Take(97).Concat("...").ToArray()) 
                        : article.Text,
                    Liked = article.Likes.Select(l => l.Id).Contains(currentUser.Id),
                    LikesCount = article.Likes.Count,
                    CommentsCount = article.Comments.Count,
                    IsPublished = article.IsPublished,
                    AuthorId = article.AuthorId
                });

            return new PagedListResult
            {
                Items = { mappedItems },
                Page = request.Page,
                Size = request.Size,
                RowsCount = rowsCount,
                TotalPages = (int)Math.Ceiling((double)rowsCount / request.Size)
            };
        }
    }
}