namespace PersonalBlog.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string objectName, object key) : base($"Entity {objectName}({key}) not found.") { }
    public NotFoundException(string massage) : base(massage) { }
}