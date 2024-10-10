using Grpc.Core;
using MediatR;
using Personal_blog;
using PersonalBlog.Core.Handlers.Blog;

namespace PersonalBlog.Api.Services;

public class BlogService(ILogger<AuthService> logger, IMediator mediator) : Blog.BlogBase
{
    public override async Task<IdMassage> create(CreateRequest request, ServerCallContext context)
    {
        var command = new Create.Command
        {
            Title = request.Title,
            Text = request.Text
        };
        return new IdMassage
        {
            Id = await mediator.Send(command)
        };
    }
}