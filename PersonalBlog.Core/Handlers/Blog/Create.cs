using MediatR;
using PersonalBlog.Core.Interfaces;

namespace PersonalBlog.Core.Handlers.Blog;

public class Create
{
    public class Command : IRequest<long>
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class Handler(ICurrentUser currentUser) : IRequestHandler<Command, long>
    {
        public async Task<long> Handle(Command request, CancellationToken cancellationToken)
        {
            return currentUser.Id;
        }
    }
}