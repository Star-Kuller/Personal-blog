using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Exceptions;
using PersonalBlog.Core.Extensions;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;
using PersonalBlogGRpc;

namespace PersonalBlog.Core.Handlers.Articles;

public class Edit
{
    public record Command(ArticleEdit Form) : IRequest;
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Form.Title).NotEmpty().MinimumLength(1).MaximumLength(255);
        }
    }
    
    public class Handler(ICurrentUser currentUser, IPbDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var form = request.Form;
            var article = await context.Articles
                              .Where(x => x.AuthorId == currentUser.Id || currentUser.IsAdmin)
                              .Where(x => x.IsPublished)
                              .NotDeleted()
                              .WithId(form.Id)
                              .FirstOrDefaultAsync(cancellationToken)
                          ?? throw new NotFoundException(nameof(Article));
            article.Name = form.Title;
            article.Text = form.Text;
        }
    }
}