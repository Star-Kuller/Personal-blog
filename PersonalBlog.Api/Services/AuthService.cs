using Grpc.Core;
using MediatR;
using PersonalBlog.Core.Handlers.auth;
using PersonalBlogGRpc;

namespace PersonalBlog.Api.Services;

public class AuthService(ILogger<AuthService> logger, IMediator mediator) : Authorization.AuthorizationBase
{
    public override async Task<TokenResponse> login(LoginForm request, ServerCallContext context)
    {
        return await mediator.Send(new Login.Query(request));
    }

    public override async Task<TokenResponse> register(RegisterForm request, ServerCallContext context)
    {
        return await mediator.Send(new Register.Query(request));
    }
}