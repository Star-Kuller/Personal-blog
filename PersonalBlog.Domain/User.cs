using PersonalBlog.Domain.Common;

namespace PersonalBlog.Domain;

public class User : UpdatableEntity
{
    public string AccountName { get; set; }
    public string Name { get; set; }
    public Role Role { get; set; }
    public string PasswordHash { get; set; }
    public bool IsBaned { get; set; }
}

public enum Role
{
    Admin = 1,
    Guest = 2
}