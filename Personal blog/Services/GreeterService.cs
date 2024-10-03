using Grpc.Core;
using Personal_blog;

namespace Personal_blog.Services;

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