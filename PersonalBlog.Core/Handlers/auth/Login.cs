using MediatR;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Handlers.auth;

public class Login
{
    public class Query : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, string>
    {
        private readonly ITokenProvider _tokenProvider;

        public Handler(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public async Task<string> Handle(Query request, CancellationToken cancellationToken)
        {
            return _tokenProvider.GetToken(request.Username, Role.Admin);
        }
    }
}