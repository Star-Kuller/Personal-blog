using Microsoft.EntityFrameworkCore;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Interfaces;

public interface IPbDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Article> Articles { get; set; }
    DbSet<Comment> Comments { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}