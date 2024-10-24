using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Exception;
using PersonalBlog.Core.Extensions;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Handlers.Blog;

public class Create
{
    public record Command(string Title, string Text) : IRequest<long>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(1).MaximumLength(255);
        }
    }
    
    public class Handler(ICurrentUser currentUser, IPbDbContext context) : IRequestHandler<Command, long>
    {
        public async Task<long> Handle(Command request, CancellationToken cancellationToken)
        {
            if (currentUser.Role != Role.Admin)
                throw new PermissionDeniedException(Role.Admin);

            var author = await GetAuthor(cancellationToken);
            
            var article = new Article
            {
                Name = request.Title,
                Text = request.Text,
                AuthorId = author.Id,
                Author = author
            };
            
            context.Articles.Add(article);
            await context.SaveChangesAsync(cancellationToken);
            
            return article.Id;
        }

        private async Task<User> GetAuthor(CancellationToken cancellationToken)
        {
            return await context.Users
                       .Where(x => !x.IsBaned)
                       .NotDeleted()
                       .WithId(currentUser.Id)
                       .FirstOrDefaultAsync(cancellationToken) 
                   ?? throw new NotFoundException(nameof(User), currentUser.Id);
        }
    }
}