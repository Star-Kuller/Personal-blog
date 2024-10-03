using Grpc.Core;

namespace Personal_blog.Services;

public class AuthService(ILogger<AuthService> logger) : Authorization.AuthorizationBase
{
    private readonly ILogger<AuthService> _logger = logger;

    public override Task<TokenResponse> login(LoginForm request, ServerCallContext context)
    {
        logger.LogInformation($"User: {request.Username} Password: {request.Password}");
        foreach (var header in context.RequestHeaders)
        {
            logger.LogInformation($"key: {header.Key} value: {header.Value}");
        }
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