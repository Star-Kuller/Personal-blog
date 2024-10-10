using System.IdentityModel.Tokens.Jwt;
using PersonalBlog.Core.Interfaces;

namespace PersonalBlog.Api.DIFactories;

public interface ICurrentUserFactory
{
    Task<ICurrentUser> CreateCurrentUserAsync(JwtSecurityToken jsonToken, HttpContext context);
}