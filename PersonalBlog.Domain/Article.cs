using PersonalBlog.Domain.Common;
using PersonalBlog.Domain.Interfaces;

namespace PersonalBlog.Domain;

public class Article : CreatableEntity, IName, IAuthor
{
    public string Name { get; set; }
    public string Text { get; set; }
    public List<string> Files { get; set; }
    public bool IsPublished { get; set; }
    
    public List<User> Likes { get; set; }
    
    public User Author { get; set; }
    public long AuthorId { get; set; }

    public List<Comment> Comments { get; set; }
}