namespace PersonalBlog.Domain.Interfaces;

public interface IAuthor
{
    User Author { get; set; }
    long AuthorId { get; set; }
}