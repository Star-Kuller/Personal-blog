namespace PersonalBlog.Core.Exception;

public class NotFoundException : System.Exception
{
    public NotFoundException(string objectName, object key) : base($"Entity {objectName}({key}) not found.") { }
    public NotFoundException(string massage) : base(massage) { }
}