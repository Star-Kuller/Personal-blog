using PersonalBlog.Domain;

namespace PersonalBlog.Core.Interfaces;

public interface ITokenProvider
{
    string GetToken(string accountName, Role role);
}