using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Exceptions;
using PersonalBlog.Core.Extensions;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Handlers.Articles;

public class Delete
{
    public record Command(long Id) : IRequest;

    public class Handler(ICurrentUser currentUser, IPbDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var article = await context.Articles
                              .Where(x => x.AuthorId == currentUser.Id || currentUser.IsAdmin)
                              .NotDeleted()
                              .WithId(request.Id)
                              .FirstOrDefaultAsync(cancellationToken)
                          ?? throw new NotFoundException(nameof(Article));

            article.Delete();

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}