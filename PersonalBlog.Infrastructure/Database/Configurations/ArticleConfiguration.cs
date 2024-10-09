using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain;

namespace PersonalBlog.Infrastructure.Database.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasOne(a => a.Author)
            .WithMany(u => u.Articles);
        
        builder.HasMany(a => a.Comments)
            .WithOne(c => c.Article);
        
        builder.HasMany(a => a.Likes)
            .WithMany(u => u.ArticleLikes);
    }
}