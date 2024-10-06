using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Security;
using PersonalBlog.Domain;

namespace PersonalBlog.Infrastructure.Database.Infrastructure;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider services, CancellationToken cancellationToken = default)
    {
        var context = services.GetRequiredService<PbDbContext>();
        var adminUserSettings = services.GetService<IOptions<AdminUser>>()?.Value;
        if(adminUserSettings is not null) 
            adminUserSettings.Salt = services.GetService<IOptions<TokenOptions>>()?.Value.Secret;
        
        await context.Database.MigrateAsync(cancellationToken);
        
        try
        {
            if(adminUserSettings is not null)
                await SetAdminUserAsync(context, adminUserSettings, cancellationToken);
        }
        catch
        {
            throw new Exception("Create or update first admin user error.");
        }
    }

    private static async Task SetAdminUserAsync(IPbDbContext context,
        AdminUser user, CancellationToken cancellationToken = default)
    {
        var admin = await context.Users
            .Where(u => u.Role == Role.Admin && u.Id == 1)
            .FirstOrDefaultAsync(cancellationToken);

        if (admin is null)
        {
            var newAdmin = new User
            {
                Role = Role.Admin,
                Username = user.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password + user.Salt)
            };
            context.Users.Add(newAdmin);
        }
        else
        {
            admin.Role = Role.Admin;
            admin.Username = user.Username;
            admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password + user.Salt);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
    
    public class AdminUser
    {
        public string? Salt { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}