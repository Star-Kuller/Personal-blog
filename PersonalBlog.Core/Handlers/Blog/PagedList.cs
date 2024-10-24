using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Common;
using PersonalBlog.Core.Extensions;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Handlers.Blog;

public class PagedList
{
    public record Query(int Page, int Size) : PagedListQuery(Page, Size), IRequest<PagedList<ListItem>>;

    public record ListItem(
        long Id,
        string Title,
        string Text,
        bool IsLiked,
        long LikesCount,
        long CommentsCount,
        bool IsPublished,
        long AuthorId)
    {
        public ListItem(Article article, long currentUserId) : this(
            article.Id,
            Title: article.Name,
            Text: article.Text.Length > 100
                ? new string(article.Text.Take(97).Concat("...").ToArray()) 
                : article.Text,
            IsLiked: article.Likes.Select(l => l.Id).Contains(currentUserId),
            article.Likes.Count,
            article.Comments.Count,
            article.IsPublished,
            article.AuthorId) {}
    };

    public class Handler(ICurrentUser currentUser, IPbDbContext context) : IRequestHandler<Query, PagedList<ListItem>>
    {
        public async Task<PagedList<ListItem>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Articles
                .Include(x => x.Comments)
                .Include(x => x.Likes)
                .Where(x => x.IsPublished || x.AuthorId == currentUser.Id)
                .NotDeleted()
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new ListItem(x, currentUser.Id))
                .ToPagedListAsync(request, cancellationToken);
        }
    }
}