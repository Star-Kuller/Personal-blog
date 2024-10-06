using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonalBlog.Core.Exception;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Security;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Handlers.auth;

public class Login
{
    public class Query : IRequest<string>
    {
        public string AccountName { get; set; }
        public string Password { get; set; }
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
            
            return tokenProvider.GetToken(request.AccountName, Role.Admin);
        }
    }
}