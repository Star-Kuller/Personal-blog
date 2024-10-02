using Grpc.Core;
using Personal_blog;

namespace Personal_blog.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Got from frontend: {request.Name}");
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name + ". I'm backend."
        });
    }
}