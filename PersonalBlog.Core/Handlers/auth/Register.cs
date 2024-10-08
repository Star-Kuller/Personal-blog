using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Exception;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Handlers.auth;

public class Register
{
    public class Query : IRequest<string>
    {
        public string DisplayName { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
    }
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.DisplayName).NotEmpty().MinimumLength(2).MaximumLength(255);
            RuleFor(x => x.AccountName).NotEmpty().MinimumLength(2).MaximumLength(255);
            RuleFor(x => x.AccountName).Matches("^[а-яА-ЯёЁa-zA-Z0-9-_]+$").WithMessage("Only A-Z, А-Я, 0-9, - and _");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(255);
        }
    }
    
    public class Handler(ITokenProvider tokenProvider, IPbDbContext context) : IRequestHandler<Query, string>
    {
        public async Task<string> Handle(Query request, CancellationToken cancellationToken)
        {
            if (await context.Users.AnyAsync(x => x.AccountName.ToLower() == request.AccountName.ToLower(), cancellationToken))
                throw new AlreadyExistException("This account name is already in use, please choose another one.");
           
            var newUser = new User
            {
                AccountName = request.AccountName,
                Name = request.DisplayName,
                Role = Role.Guest,
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password)
            };
            context.Users.Add(newUser);
            await context.SaveChangesAsync(cancellationToken);
            
            return tokenProvider.GetToken(request.AccountName, Role.Guest);
        }
    }
}