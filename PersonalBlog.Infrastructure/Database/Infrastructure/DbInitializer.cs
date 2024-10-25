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
            .Where(u => u.Role == Role.Admin)
            .FirstOrDefaultAsync(cancellationToken);

        if (admin is null)
        {
            var newAdmin = new User(
                user.AccountName,
                Role.Admin, 
                passwordHash: BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password));
            context.Users.Add(newAdmin);
        }
        else
        {
            admin.Role = Role.Admin;
            admin.AccountName = user.AccountName;
            admin.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
    
    public class AdminUser
    {
        public string AccountName { get; set; } = "";
        public string Name { get; set; } = "";
        public string Password { get; set; } = "";
    }
}