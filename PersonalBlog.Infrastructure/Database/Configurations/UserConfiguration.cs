using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain;

namespace PersonalBlog.Infrastructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasAlternateKey(u => u.AccountName);
        
        builder.Property(u => u.PasswordHash).IsRequired();

        builder.HasMany(u => u.Articles)
            .WithOne(a => a.Author);
        
        builder.HasMany(u => u.ArticleLiked)
            .WithMany(a => a.Likes);
        
        builder.HasMany(u => u.Comments)
            .WithOne(c => c.Author);
    }
}