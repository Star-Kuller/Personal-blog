using PersonalBlog.Domain;

namespace PersonalBlog.Core.Exception;

public class PermissionDeniedException : System.Exception
{
    public PermissionDeniedException(string message) : base(message) {}
    public PermissionDeniedException(Role role) : base($"To do this you need to be {role}") {}
}