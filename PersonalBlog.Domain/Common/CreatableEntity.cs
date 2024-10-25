namespace PersonalBlog.Domain.Common;

public abstract class CreatableEntity : Entity
{
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; private set; }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}