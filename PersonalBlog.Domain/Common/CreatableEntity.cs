namespace PersonalBlog.Domain.Common;

public abstract class CreatableEntity : Entity
{
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}