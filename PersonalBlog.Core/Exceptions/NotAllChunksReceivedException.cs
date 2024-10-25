namespace PersonalBlog.Core.Exceptions;

public class NotAllChunksReceivedException : Exception
{
    public NotAllChunksReceivedException(int received, int total) : base($"Not all chunks received {received}/{total}") { }
    public NotAllChunksReceivedException(string massage) : base(massage) { }
}