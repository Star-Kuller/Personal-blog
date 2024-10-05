using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Personal_blog;

namespace PersonalBlog.Api.Services;

[Authorize]
public class GreeterService(ILogger<GreeterService> logger) : Greeter.GreeterBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        logger.LogInformation($"Got from frontend: {request.Name}");
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name + ". I'm backend."
        });
    }
}