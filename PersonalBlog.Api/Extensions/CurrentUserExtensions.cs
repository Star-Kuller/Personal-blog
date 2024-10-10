using PersonalBlog.Api.DIFactories;
using PersonalBlog.Api.Middlewares;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Security;

namespace PersonalBlog.Api.Extensions;

public static class CurrentUserExtensions
{
    public static IServiceCollection AddCurrentUser(this IServiceCollection  services)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddSingleton<ICurrentUserFactory, CurrentUserFactory>();
        return services;
    }
    
    public static IApplicationBuilder UseCurrentUser(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtDecoderMiddleware>();
    }
}