using PersonalBlog.Domain.Common;
using PersonalBlog.Domain.Interfaces;

namespace PersonalBlog.Domain;

public class Article : CreatableEntity, IName, IAuthor
{
    public string Name { get; set; }
    
    public string Message { get; set; }
    public List<string> Files { get; set; }
    
    public List<User> Likes { get; set; }
    
    public User Author { get; set; }
    public long AuthorId { get; set; }

    public List<Comment> Comments { get; set; }
}