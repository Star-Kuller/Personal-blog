using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Personal_blog;
using PersonalBlog.Core.Handlers.Blog;
using PagedList = PersonalBlog.Core.Handlers.Blog.PagedList;

namespace PersonalBlog.Api.Services;

[Authorize]
public class BlogService(ILogger<AuthService> logger, IMediator mediator) : Blog.BlogBase
{
    public override async Task<IdMassage> create(ArticleCreate request, ServerCallContext context)
    {
        return new IdMassage
        {
            Id = await mediator.Send(new Create.Command(request.Title, request.Text))
        };
    }
    
    public override async Task<Empty> edit(ArticleEdit request, ServerCallContext context)
    {
        return await base.edit(request, context);
    }

    public override async Task<ArticleInfo> view(IdMassage request, ServerCallContext context)
    {
        var result = await mediator.Send(new View.Query(request.Id));
        return new ArticleInfo
        {
            Id = result.Id,
            Title = result.Title,
            Text = result.Text,
            AuthorId = result.AuthorId,
            IsPublished = result.IsPublished,
            Liked = result.IsLiked,
            LikesCount = result.LikesCount,
            CommentsCount = result.CommentsCount,
            Comments = 
            { 
                result.Comments.Select(x => 
                new Comment
                { 
                    AuthorId = x.AuthorId, 
                    AuthorName = x.AuthorName, 
                    AuthorAvatar = x.AuthorAvatar, 
                    Message = x.Message 
                }) 
            },
            MediaUrls = { result.MediaUrls }
        };
    }

    public override async Task<PagedListResult> getPagedList(PagedListQuery request, ServerCallContext context)
    {
        var result = await mediator.Send(new PagedList.Query(request.Page, request.Size));
        return new PagedListResult
        {
            Page = result.Page,
            Size = result.Size,
            TotalPages = result.TotalPages,
            RowsCount = result.RowsCount,
            Items = 
            { 
                result.Items.Select(x => 
                new ArticleShort
                {
                    Id = x.Id,
                    Title = x.Title,
                    Text = x.Text,
                    AuthorId = x.AuthorId,
                    CommentsCount = x.CommentsCount,
                    LikesCount = x.LikesCount,
                    IsPublished = x.IsPublished,
                    Liked = x.IsLiked,
                }) 
            }
        };
    }
}