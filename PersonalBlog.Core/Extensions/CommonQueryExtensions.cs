using PersonalBlog.Domain.Common;
using PersonalBlog.Domain.Interfaces;

namespace PersonalBlog.Core.Extensions;

public static class CommonQueryExtensions
{
    public static IQueryable<T> WithId<T>(this IQueryable<T> query, long id)
        where T : Entity
    {
        return query.Where(x => x.Id == id);
    }
    
    public static IQueryable<T> NotDeleted<T>(this IQueryable<T> query)
        where T : CreatableEntity
    {
        return query.Where(x => !x.IsDeleted);
    }
    
    public static IQueryable<T> AuthorIdIs<T>(this IQueryable<T> query, long id)
        where T : IAuthor
    {
        return query.Where(x => x.AuthorId == id);
    }

    public static IQueryable<T> Count<T>(this IQueryable<T> query, out int count)
    {
        count = query.Count();
        return query;
    }
}