using PersonalBlog.Domain.Common;
using PersonalBlog.Domain.Interfaces;

namespace PersonalBlog.Domain;

public class Comment : CreatableEntity, IAuthor
{
    public string Message { get; set; }
    public List<string> Files { get; set; }
    
    public List<User> Likes { get; set; }
    
    public Article Article { get; set; }
    public long ArticleId { get; set; }
    
    public User Author { get; set; }
    public long AuthorId { get; set; }
}