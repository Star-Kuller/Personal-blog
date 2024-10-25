using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain;

namespace PersonalBlog.Infrastructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.AccountName).IsUnique();

        builder.HasMany(u => u.Articles)
            .WithOne(a => a.Author);
        builder.HasMany(u => u.ArticleLiked)
            .WithMany(a => a.Likes);
        
        builder.HasMany(u => u.Comments)
            .WithOne(c => c.Author);
    }
}