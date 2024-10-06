using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PersonalBlog.Core.Security;

public class TokenOptions
{
    public string? Secret { get; set; }

    public SymmetricSecurityKey SecurityKey => new (Encoding.UTF8.GetBytes(Secret));

    public string Issuer { get; set; }

    public string Audience { get; set; }

    /// <summary>
    /// Access token lifetime in days
    /// </summary>        
    public int Lifetime { get; set; }
}