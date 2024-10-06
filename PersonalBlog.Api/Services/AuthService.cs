using Grpc.Core;
using MediatR;
using Personal_blog;
using PersonalBlog.Core.Handlers.auth;

namespace PersonalBlog.Api.Services;

public class AuthService(ILogger<AuthService> logger, IMediator mediator) : Authorization.AuthorizationBase
{
    public override async Task<TokenResponse> login(LoginForm request, ServerCallContext context)
    {
        logger.LogInformation("User: {RequestUsername} Password: {RequestPassword}", request.AccountName, request.Password);
        var query = new Login.Query()
        {
            AccountName = request.AccountName,
            Password = request.Password
        };
        
        return new TokenResponse()
        {
            Token = await mediator.Send(query)
        };
    }

    public override Task<TokenResponse> register(RegisterForm request, ServerCallContext context)
    {
        return Task.FromResult(new TokenResponse
        {
            Token = ""
        });
    }
}