using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Exceptions;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;
using PersonalBlogGRpc;

namespace PersonalBlog.Core.Handlers.auth;

public class Register
{
    public record Query(RegisterForm Form) : IRequest<TokenResponse>;
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Form.Name).NotEmpty().MinimumLength(2).MaximumLength(255);
            RuleFor(x => x.Form.AccountName).NotEmpty().MinimumLength(2).MaximumLength(255);
            RuleFor(x => x.Form.AccountName).Matches("^[а-яА-ЯёЁa-zA-Z0-9-_]+$").WithMessage("Only A-Z, А-Я, 0-9, - and _");
            RuleFor(x => x.Form.Password).NotEmpty().MinimumLength(6).MaximumLength(255);
        }
    }
    
    public class Handler(ITokenProvider tokenProvider, IPbDbContext context) : IRequestHandler<Query, TokenResponse>
    {
        public async Task<TokenResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            var form = request.Form;
            
            if (await context.Users.AnyAsync(x => 
                    x.AccountName.ToLower() == form.AccountName.ToLower()
                    , cancellationToken))
                throw new AlreadyExistException(
                    "This account name is already in use, please choose another one.");
           
            var newUser = new User
            {
                AccountName = form.AccountName,
                Name = form.Name,
                Role = Role.Guest,
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(form.Password)
            };
            context.Users.Add(newUser);
            await context.SaveChangesAsync(cancellationToken);

            return new TokenResponse
            {
                Token = tokenProvider.GetToken(form.AccountName, Role.Guest)
            };
        }
    }
}