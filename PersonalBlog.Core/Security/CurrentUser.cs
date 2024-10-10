using System.Net;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;

namespace PersonalBlog.Core.Security;

public class CurrentUser : ICurrentUser
{
    public long Id { get; set;  }
    public string Name { get; set;  }
    public string AccountName { get; set; }
    public Role Role { get; set; }
    public bool IsDeleted { get; set; }
    public IPAddress IpAddress { get; set;  }
}