using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

using PersonalBlog.Core.Interfaces;

namespace PersonalBlog.Api.Middlewares;

public class CurrentUserMiddleware(RequestDelegate next,
    ILogger<CurrentUserMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext, IPbDbContext dbContext, ICurrentUser currentUser)
    {
        if (!(httpContext.User.Identity?.IsAuthenticated ?? false))
        {
            await next(httpContext);
            return;
        }
        
        var claims = httpContext.User.Claims;
        var accountName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.AccountName == accountName);
        logger.LogInformation("Id:{UserId} Account: {UserAccountName} Role: {UserRole}", user.Id, user.AccountName, user.Role);
        currentUser.Id = user.Id;
        currentUser.AccountName = user.AccountName;
        currentUser.Name = user.Name;
        currentUser.Role = user.Role;
        currentUser.IsDeleted = user.IsDeleted;
        currentUser.IpAddress = httpContext.Connection.RemoteIpAddress;

        await next(httpContext);
    }
}