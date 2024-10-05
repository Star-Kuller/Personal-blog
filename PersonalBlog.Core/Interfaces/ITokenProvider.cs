using PersonalBlog.Core.Common;

namespace PersonalBlog.Core.Interfaces;

public interface ITokenProvider
{
    string GetToken(string username, Role role);
}