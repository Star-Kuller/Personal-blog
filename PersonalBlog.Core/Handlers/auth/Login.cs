using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Exception;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Handlers.auth;

public class Login
{
    public record Query(string AccountName, string Password) : IRequest<string>;
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.AccountName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
    
    public class Handler(ITokenProvider tokenProvider, IPbDbContext context) : IRequestHandler<Query, string>
    {
        public async Task<string> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .Where(x => !x.IsBaned && !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.AccountName == request.AccountName, cancellationToken);
            
            if (user is null || !BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash))
                throw new NotFoundException($"User with same password and account name not found.");
            
            return tokenProvider.GetToken(request.AccountName, user.Role);
        }
    }
}