using System.Net;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Interfaces;

public interface ICurrentUser
{
    public long Id { get;}
    public string Name { get; }
    public string AccountName { get; }
    public Role Role { get; }
    public bool IsDeleted { get; }
    public IPAddress IpAddress { get; }
}