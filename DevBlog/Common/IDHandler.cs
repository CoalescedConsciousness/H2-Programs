namespace Common;
public static class IDHandler 
{
    public static int PostID;
    public static int AuthorID;

    public static int SetPostID()
    {
        ++PostID;
        return PostID;
    }

    public static int SetAuthorID()
    {
        ++AuthorID;
        return AuthorID;
    }
}