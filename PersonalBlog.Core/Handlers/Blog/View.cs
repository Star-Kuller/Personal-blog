using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Exception;
using PersonalBlog.Core.Extensions;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Handlers.Blog;

public class View
{
    public record Query(long Id) : IRequest<Result>;

    public record Result(
        long Id, 
        string Title, 
        string Text, 
        bool IsPublished, 
        bool IsLiked, 
        long LikesCount,
        long CommentsCount,
        long AuthorId,
        IEnumerable<string> MediaUrls,
        IEnumerable<Comment> Comments)
    {
        public Result(Article article, long currentUserId) : this
        (
            article.Id,
            Title: article.Name,
            article.Text,
            article.IsPublished,
            IsLiked: article.Likes.Select(l => l.Id).Contains(currentUserId),
            article.Likes.Count,
            article.Comments.Count,
            article.AuthorId,
            article.MediaUrls,
            article.Comments
                .Where(c => !c.IsDeleted)
                .Select(c =>
                    new Comment(
                        c.AuthorId,
                        c.Author.Name,
                        c.Author.Base64Avatar,
                        c.Message))
        ){}
    }

    public record Comment(long AuthorId, string AuthorName, string? AuthorAvatar, string Message);
    
    public class Handler(ICurrentUser currentUser, IPbDbContext context) : IRequestHandler<Query, Result>
    {
        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Articles
                       .Include(x => x.Comments)
                       .ThenInclude(x => x.Author)
                       .Include(x => x.Likes)
                       .Where(x => x.AuthorId == currentUser.Id || x.IsPublished)
                       .Where(x => !x.IsDeleted || currentUser.Role == Role.Admin)
                       .WithId(request.Id)
                       .Select(x => new Result(x, currentUser.Id))
                       .FirstOrDefaultAsync(cancellationToken)
                   ?? throw new NotFoundException(nameof(Article));
        }
    }
}