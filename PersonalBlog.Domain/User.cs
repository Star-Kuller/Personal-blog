using PersonalBlog.Domain.Common;
using PersonalBlog.Domain.Interfaces;

namespace PersonalBlog.Domain;

public class User : CreatableEntity, IName
{
    public string AccountName { get; set; } = "";
    public string Name { get; set; } = "";
    public Role Role { get; set; }
    public string PasswordHash { get; set; } = "";
    public string? Base64Avatar { get; set; }
    public bool IsBaned { get; set; }

    public List<Article> Articles { get; set; }
    public List<Article> ArticleLiked { get; set; }
    public List<Comment> Comments { get; set; }
}

public enum Role
{
    Admin = 1,
    Guest = 2
}