using Microsoft.EntityFrameworkCore;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Interfaces;

public interface IPbDbContext
{
    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}