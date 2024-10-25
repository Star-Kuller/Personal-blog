using System.Net;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Interfaces;

public interface ICurrentUser
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string AccountName { get; set; }
    public Role Role { get; set; }
    public bool IsDeleted { get; set; }
    public IPAddress? IpAddress { get; set; }
    
    public bool IsAdmin { get; }
    public bool IsGuest { get; }
}