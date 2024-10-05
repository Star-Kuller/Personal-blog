using Grpc.Core;

namespace Personal_blog.Services;

public class AuthService(ILogger<AuthService> logger) : Authorization.AuthorizationBase
{
    private readonly ILogger<AuthService> _logger = logger;

    public override Task<TokenResponse> login(LoginForm request, ServerCallContext context)
    {
        logger.LogInformation($"User: {request.Username} Password: {request.Password}");
        throw new RpcException(new Status(StatusCode.InvalidArgument, "Test1"), "Test1");
        return Task.FromResult(new TokenResponse
        {
            Token = "123"
        });
    }

    public override Task<TokenResponse> register(RegisterForm request, ServerCallContext context)
    {
        return Task.FromResult(new TokenResponse
        {
            Token = ""
        });
    }
}