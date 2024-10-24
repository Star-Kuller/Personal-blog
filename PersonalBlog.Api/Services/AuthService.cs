using Grpc.Core;
using MediatR;
using Personal_blog;
using PersonalBlog.Core.Handlers.auth;

namespace PersonalBlog.Api.Services;

public class AuthService(ILogger<AuthService> logger, IMediator mediator) : Authorization.AuthorizationBase
{
    public override async Task<TokenResponse> login(LoginForm request, ServerCallContext context)
    {
        return new TokenResponse
        {
            Token = await mediator.Send(new Login.Query(request.AccountName, request.Password))
        };
    }

    public override async Task<TokenResponse> register(RegisterForm request, ServerCallContext context)
    {
        return new TokenResponse
        {
            Token = await mediator.Send(new Register.Query(request.Name, request.AccountName, request.Password))
        };
    }
}