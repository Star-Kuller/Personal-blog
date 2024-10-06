namespace PersonalBlog.Domain.Common;

public abstract class UpdatableEntity
{
    public long Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}