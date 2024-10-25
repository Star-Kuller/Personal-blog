using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain;

namespace PersonalBlog.Infrastructure.Database.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasOne(c => c.Author)
            .WithMany(u => u.Comments);
        
        builder.HasOne(c => c.Article)
            .WithMany(a => a.Comments);
    }
}