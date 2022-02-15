namespace Common;
// Deprecated but retained for the sake of proof-of-concept.
//public class ExistsException : Exception
//{
//    public ExistsException()
//        : base()
//    {
//    }

//    public ExistsException(string data) : base($"{data} Already exists")
//    {
//    }
//}

public class UnableToDelete : Exception
{
    public UnableToDelete() : base() { }

    public UnableToDelete(string id) : base($"Unable to delete {id}") { }
}

