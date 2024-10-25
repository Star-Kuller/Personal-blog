namespace PersonalBlog.Core.Exceptions;

public class AlreadyExistException(string massage) : Exception(massage);