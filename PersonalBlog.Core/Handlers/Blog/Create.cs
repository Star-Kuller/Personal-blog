using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Exception;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Handlers.Blog;

public class Create
{
    public class Command : IRequest<long>
    {
        public string Title { get; set; }
        public string Text { get; set; }
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
                    .Where(x => !x.IsDeleted)
                    .Where(x => x.Id == currentUser.Id)
                    .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(nameof(User), currentUser.Id);
        }
    }
}