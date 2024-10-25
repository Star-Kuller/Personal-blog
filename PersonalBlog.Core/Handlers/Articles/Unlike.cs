using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Exceptions;
using PersonalBlog.Core.Extensions;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Handlers.Articles;

public class Unlike
{
    public record Command(long Id) : IRequest;

    public class Handler(ICurrentUser currentUser, IPbDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var article = await context.Articles
                              .Include(x => x.Likes)
                              .Where(x => x.IsPublished)
                              .NotDeleted()
                              .WithId(request.Id)
                              .FirstOrDefaultAsync(cancellationToken)
                          ?? throw new NotFoundException(nameof(Article));

            article.Likes.RemoveAll(x => x.Id == currentUser.Id);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}