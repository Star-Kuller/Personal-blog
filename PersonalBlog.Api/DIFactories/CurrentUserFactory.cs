using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Security;

namespace PersonalBlog.Api.DIFactories;

public class CurrentUserFactory(IPbDbContext context) : ICurrentUserFactory
{
    public async Task<ICurrentUser> CreateCurrentUserAsync(JwtSecurityToken jsonToken, HttpContext httpContext)
    {
        var claims = jsonToken.Claims;
        var accountName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        var user = await context.Users.FirstOrDefaultAsync(x => x.AccountName == accountName);
        return new CurrentUser
        {
            Id = user.Id,
            AccountName = user.AccountName,
            Name = user.Name,
            Role = user.Role,
            IsDeleted = user.IsDeleted,
            IpAddress = httpContext.Connection.RemoteIpAddress
        };
    }
}