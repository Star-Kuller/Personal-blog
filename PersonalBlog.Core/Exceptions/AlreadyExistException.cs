namespace PersonalBlog.Core.Exception;

public class AlreadyExistException(string massage) : System.Exception(massage);