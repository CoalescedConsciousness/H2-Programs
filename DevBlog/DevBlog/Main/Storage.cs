using DevBlog;
using Common;

// DEPRECATED
// namespace Storage;

/// <summary>
/// Public record used to maintain database of Posts, utilizes interface to standardize
/// </summary>
//public class PostStorage : IStorage
//{
    //    internal const string PostDatabase = @"posts.txt";
    //    public static List<Post> PostDB = new List<Post>();

    //    // For the sake of concept, let's assume the Post database is off-premise, and thus I/O-bound:
    //    public static async void ReadAll()
    //    {
    //        Console.WriteLine("\n Reading all Posts: ");

    //        foreach (Post post in PostDB)
    //        {
    //            Console.WriteLine(post.ToString());
    //        }

    //    }

    //    public static void Save()
    //    {
    //        List<string> currentList = Database.GetColumn("Post", "ID");
    //        foreach (Post post in PostDB)
    //        {
    //            if (!currentList.Contains(post.ID.ToString()))
    //            {
    //                List<string> fields = new List<string>() { "Title", "Body", "Author", "AuthorID", "Links" };
    //                List<string> values = new List<string>() { post.Title, post.Body, post.Authors[0].Name, post.Authors[0].ID.ToString(), post.Links };
    //                Database.SaveToDatabase(fields, values, "Post");
    //            }
    //        }
    //    }


    //    public static async void Load()
    //    {
    //        List<object> x = await Database.GetAllFromDatabase("Post", true); // False removes writes to console.

    //        foreach (List<object> listItem in x)
    //        {
    //            List<string> valList = new List<string>();
    //            Console.WriteLine();
    //            foreach (object item in listItem)
    //            {
    //                Console.Write(item);
    //                valList.Add(item.ToString());
    //            }



    //        }
    //        Console.ReadKey();
    //    }

    //    public static void Update(int id)
    //    {
    //        Console.Clear();
    //        Post target = PostDB.Find(x => x.ID == id);

    //        Console.WriteLine("Please select the field you wish to edit (body, title, active)");
    //        string field = Console.ReadLine();

    //        Console.WriteLine("\nReplace with:");
    //        string newData = Console.ReadLine();

    //        if (field != null && newData != null && target != null)
    //        {
    //            object p = field.ToLower() switch
    //            {
    //                "body" => target.Body = newData,
    //                "title" => target.Title = newData,
    //                "active" => (newData.ToLower() == "true") ? target.Active = true : target.Active = false,
    //                _ => throw new FormatException(),
    //            };
    //        }

    //        target.EditDate = DateTime.Now;

    //        string query = $"SELECT * FROM POST WHERE ID = {id} " +
    //            $"SET {field} = {newData}";
    //        Database.QueryDatabase(query);
    //    }
    //}

    /// <summary>
    /// Used to maintain database of available Authors.
    /// </summary>


