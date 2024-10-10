using System.IdentityModel.Tokens.Jwt;
using PersonalBlog.Api.DIFactories;

namespace PersonalBlog.Api.Middlewares;

public class JwtDecoderMiddleware(RequestDelegate next,
    ILogger<JwtDecoderMiddleware> logger,
    ICurrentUserFactory currentUserFactory)
{
    public async Task InvokeAsync(HttpContext context)
    {
        string authHeader = context.Request.Headers["Authorization"];

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer ")) await next(context);
        
        var token = authHeader["Bearer ".Length..].Trim();
        try
        {
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is JwtSecurityToken jsonToken)
            {
                var user = await currentUserFactory.CreateCurrentUserAsync(jsonToken, context);
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning("Can't read JWT token");
            throw ex;
        }
        
        await next(context);
    }
}