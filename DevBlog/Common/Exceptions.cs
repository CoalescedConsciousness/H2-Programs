namespace Common;

public class ExistsException : Exception
{
    public ExistsException()
        : base()
    {
    }

    public ExistsException(string data) : base($"{data} Already exists")
    {
    }
}

