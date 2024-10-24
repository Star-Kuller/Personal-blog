namespace PersonalBlog.Core.Common;

public abstract record PagedListQuery(int Page, int Size);

public record PagedList<T>(int Page, int Size, int TotalPages, int RowsCount)
{
    public List<T> Items { get; set; } = [];

    public PagedList(PagedListQuery query, int TotalPages, int rowsCount)
        : this(query.Page, query.Size, TotalPages, rowsCount) { }
    
    public PagedList(int page, int size, int totalPages, int rowsCount, List<T> items) 
        : this(page, size, totalPages, rowsCount)
    {
        Items = items;
    }

    public PagedList(PagedListQuery query, int totalPages, int rowsCount, List<T> items) 
        : this(query.Page, query.Size, totalPages, rowsCount)
    {
        Items = items;
    }
    
}