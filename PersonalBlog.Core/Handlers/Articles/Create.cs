using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Exceptions;
using PersonalBlog.Core.Extensions;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;
using PersonalBlogGRpc;

namespace PersonalBlog.Core.Handlers.Articles;

public class Create
{
    public record Command(ArticleCreate Form) : IRequest<IdMassage>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Form.Title).NotEmpty().MinimumLength(1).MaximumLength(255);
        }
    }
    
    public class Handler(ICurrentUser currentUser, IPbDbContext context) : IRequestHandler<Command, IdMassage>
    {
        public async Task<IdMassage> Handle(Command request, CancellationToken cancellationToken)
        {
            var form = request.Form;
            if (currentUser.Role != Role.Admin)
                throw new PermissionDeniedException(Role.Admin);

            var author = await GetAuthor(cancellationToken);
            
            var article = new Article
            {
                Name = form.Title,
                Text = form.Text,
                AuthorId = author.Id,
                Author = author
            };
            
            context.Articles.Add(article);
            await context.SaveChangesAsync(cancellationToken);
            
            return new IdMassage { Id = article.Id };
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