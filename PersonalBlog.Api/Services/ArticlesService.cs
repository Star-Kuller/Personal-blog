using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PersonalBlogGRpc;
using PersonalBlog.Core.Handlers.Articles;
using PagedList = PersonalBlog.Core.Handlers.Articles.PagedList;

namespace PersonalBlog.Api.Services;

[Authorize]
public class ArticlesService(ILogger<AuthService> logger, IMediator mediator) : Articles.ArticlesBase
{
    public override async Task<PagedListResult> getPagedList(PagedListQuery request, ServerCallContext context)
    {
        return await mediator.Send(new PagedList.Query(request));
    }
    
    public override async Task<IdMassage> create(ArticleCreate request, ServerCallContext context)
    {
        return await mediator.Send(new Create.Command(request));
    }
    
    public override async Task<Empty> edit(ArticleEdit request, ServerCallContext context)
    {
        await mediator.Send(new Edit.Command(request));
        return new Empty();
    }

    public override async Task<Empty> delete(IdMassage request, ServerCallContext context)
    {
        await mediator.Send(new Delete.Command(request.Id));
        return new Empty();
    }

    public override async Task<ArticleInfo> view(IdMassage request, ServerCallContext context)
    {
        return await mediator.Send(new View.Query(request.Id));
    }

    public override async Task<Empty> uploadFile(IAsyncStreamReader<FileChunk> requestStream, ServerCallContext context)
    {
        await mediator.Send(new UploadFile.Command(requestStream, context));
        return new Empty();
    }

    public override async Task<Empty> removeFile(RemoveFile request, ServerCallContext context)
    {
        await mediator.Send(new UploadFile.Command(null, context));
        return new Empty();
    }

    public override async Task<Empty> publish(IdMassage request, ServerCallContext context)
    {
        await mediator.Send(new Publish.Command(request.Id));
        return new Empty();
    }

    public override async Task<Empty> like(IdMassage request, ServerCallContext context)
    {
        await mediator.Send(new Like.Command(request.Id));
        return new Empty();
    }
    
    public override async Task<Empty> unlike(IdMassage request, ServerCallContext context)
    {
        await mediator.Send(new Unlike.Command(request.Id));
        return new Empty();
    }
}