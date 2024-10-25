using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Exceptions;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;
using PersonalBlogGRpc;

namespace PersonalBlog.Core.Handlers.auth;

public class Login
{
    public record Query(LoginForm Form) : IRequest<TokenResponse>;
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Form.AccountName).NotEmpty();
            RuleFor(x => x.Form.Password).NotEmpty();
        }
    }
    
    public class Handler(ITokenProvider tokenProvider, IPbDbContext context) : IRequestHandler<Query, TokenResponse>
    {
        public async Task<TokenResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            var form = request.Form;
            
            var user = await context.Users
                .Where(x => !x.IsBaned && !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.AccountName == form.AccountName, cancellationToken);
            
            if (user is null || !BCrypt.Net.BCrypt.EnhancedVerify(form.Password, user.PasswordHash))
                throw new NotFoundException($"User with same password and account name not found.");
            
            return new TokenResponse
            {
                Token = tokenProvider.GetToken(form.AccountName, user.Role)
            };
        }
    }
}