using PersonalBlog.Api.Middlewares;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Security;

namespace PersonalBlog.Api.Extensions;

public static class CurrentUserExtensions
{
    public static IServiceCollection AddCurrentUser(this IServiceCollection  services)
    {
        return services.AddScoped<ICurrentUser, CurrentUser>();
    }
    
    public static IApplicationBuilder UseCurrentUser(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CurrentUserMiddleware>();
    }
}