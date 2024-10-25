using PersonalBlog.Domain.Common;
using PersonalBlog.Domain.Interfaces;

namespace PersonalBlog.Domain;

public class User(string accountName, Role role, string passwordHash) : CreatableEntity, IName
{
    public string AccountName { get; set; } = accountName;
    public string Name { get; set; } = accountName;
    public Role Role { get; set; } = role;
    public string PasswordHash { get; set; } = passwordHash;
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