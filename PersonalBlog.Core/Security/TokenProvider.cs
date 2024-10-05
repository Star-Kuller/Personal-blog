using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PersonalBlog.Core.Common;
using PersonalBlog.Core.Interfaces;

namespace PersonalBlog.Core.Security;

public class TokenProvider(IOptions<TokenOptions> options) : ITokenProvider
{
    private readonly TokenOptions _options = options.Value;

    public string GetToken(string username, Role role)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new InvalidOperationException("Name is not specified.");
        }
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            new []
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role.ToString())
            },
            expires: DateTime.Now.AddDays(_options.Lifetime),
            signingCredentials: new SigningCredentials(_options.SecurityKey, SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}