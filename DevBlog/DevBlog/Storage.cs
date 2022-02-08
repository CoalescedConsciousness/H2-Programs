using DevBlog;
using Common;

namespace Storage;

/// <summary>
/// Public record used to maintain database of Posts, utilizes interface to standardize
/// </summary>
public class PostStorage : IStorage
{
    internal const string PostDatabase = @"posts.txt";
    public static List<Post> PostDB = new List<Post>();

    // For the sake of concept, let's assume the Post database is off-premise, and thus I/O-bound:
    public static async void ReadAll()
    {
        Console.WriteLine("\n Reading all Posts: ");

        while (!Task.Run(() => Database.GetAllFromDatabase("Post")).IsCompleted)
        {
            Console.Write(".");
        }

    }

    public static void Save()
    {
        List<string> currentList = Database.GetColumn("Post", "ID");
        foreach (Post post in PostDB)
        {
            if (!currentList.Contains(post.ID.ToString()))
            {
                List<string> fields = new List<string>() { "Title", "Body" };
                List<string> values = new List<string>() { post.Title, post.Body };

                Database.SaveToDatabase(fields, values, "Post");
            }
        }
    }


    public static async void Load()
    {
        Task<List<object>> x = Database.GetAllFromDatabase("Post", "false");
       
        for (int i = 0; i < x.Result.Count; i++) /// HERE BE TROUBLE
        {
            
            Console.WriteLine(x.Result[i].ToString());
        }
        Console.ReadKey();
    }

    public static void Update(Post post, string field, string newData)
    {
        Post target = PostDB.Find(x => x.ID == post.ID);
            
        object p = field.ToLower() switch
        {
            "body" => target.Body = newData,
            "title" => target.Title = newData,
            "active" => (newData.ToLower() == "true") ? target.Active = true : target.Active = false,
            _ => throw new FormatException(),
        };

        target.EditDate = DateTime.Now;

    }
}

/// <summary>
/// Used to maintain database of available Authors.
/// </summary>
public class AuthorStorage : IStorage
{
    internal static string AuthorDatabase = @"authors.txt";
    public static List<Author> AuthorDB = new List<Author>();
    public static async void ReadAll()
    {
        Console.WriteLine("\nLoading database");
        Task<List<object>> list = Database.GetAllFromDatabase("Author");
        
        while (!list.IsCompleted) 
        {
            Console.Write(".");
        }

        List<object> aList = await list;
    }


    public static void Save()
    {
        List<string> currentList = Database.GetColumn("Author", "ID");
        foreach (Author author in AuthorDB)
        {
            if (!currentList.Contains(author.ID.ToString()))
            {
                List<string> fields = new List<string>() { "Name", "Email", "PostCount", "Active" };
                List<string> values = new List<string>() { author.Name, author.Email, author.PostCount.ToString(), author.Active.ToString() };

                Database.SaveToDatabase(fields, values, "Author");
            }
        }
    }
 
    public static void Load()
    {
        
        {
            foreach (Author author in AuthorDB)
            {
                
            }
        }
    }

    public static void Update(Author author, string field, string newData)
    {
        Author target = AuthorDB.Find(x => x.ID == author.ID);
        object p = field.ToLower() switch
        {
            "name" => target.Name = newData,
            "email" => target.Email = newData,
            "active" => (field.ToLower() == "true") ? target.Active = true : target.Active = false,
            _ => throw new FormatException(),
        };
    }
}
