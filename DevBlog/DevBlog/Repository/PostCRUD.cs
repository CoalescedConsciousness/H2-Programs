using DevBlog;
using Common;

namespace DevBlog.Repository
{
    public class PostCRUD
    {
        /// <summary>
        /// Create method for Post objects, which increments Author PostCount.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Post CreatePost(string[] args)
        {
            if (args != null)
            {
                Post p = new Post();
                if (args.Length == 7)
                {
                    p.ID = int.Parse(args[0]);
                    p.Title = args[1];
                    p.Body = args[2];
                    p.Authors = args[3];
                    p.AuthorIDs = args[4];
                    p.Links = args[5];
                    p.Active = args[6] == "true";
                    Author a = Author.GetAuthorByID(int.Parse(p.AuthorIDs));
                    AuthorCRUD.UpdatePostCount(a);
                }
                if (args.Length == 3)
                {
                    Author a = Author.GetAuthorByID(int.Parse(args[0]));
                    p.Body = args[1];
                    p.Title = args[2];
                    p.Authors += a.Name;
                    p.AuthorIDs += a.ID;
                    p.Active = true;
                    AuthorCRUD.UpdatePostCount(a);

                }

                SaveToBoth(p);
                return p;
            }
            else { throw new Exception("Invalid creation method."); }
        }

        /// <summary>
        /// Small method to ensure saves occur to both volatile and permanent storage.
        /// </summary>
        /// <param name="a"></param>
        internal static void SaveToBoth(Post p)
        {
            Post.PostDB.Add(p);
            Save();
        }

        /// <summary>
        /// Method for UI-based post creation, which ultimately calls the (overloaded) CreatePost method.
        /// </summary>
        public static void CreatePost()
        {
            Console.Clear();
            Console.WriteLine("Please designate which Author is writing:");
            List<string> ids = DatabaseHelper.GetColumn("Author");
            List<string> names = DatabaseHelper.GetColumn("Author", "Name");

            var ts = ids.Zip(names);
            foreach (var x in ts)
            {
                Console.WriteLine($"{x.First} {x.Second}");
            }
            foreach (Author x in Author.AuthorDB)
            {
                Console.WriteLine(x.Name, x.ID);
            }
            string input = Console.ReadLine();
            int aInput = default;
            try
            {
                aInput = int.Parse(input);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Input was not an integer. Aborting.");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Title of your post: ");
            string tInput = Console.ReadLine();
            Console.WriteLine("Write the main body of your post:");
            string bInput = Console.ReadLine();
            string[] l = new string[]
            {
                aInput.ToString(),
                bInput,
                tInput,
            };
            CreatePost(l);

        }


        

        /// <summary>
        /// Saves Posts to permanent storage.
        /// </summary>
        public static void Save()
        {
            List<string> currentList = DatabaseHelper.GetColumn("Post", "ID");

            foreach (Post post in Post.PostDB)
            {
                if (currentList.Count == 0 || !currentList.Contains((post.ID).ToString()))
                {
                    List<string> fields = new List<string>() { "Title", "Body", "Author", "AuthorID", "Links", "Active" };
                    List<string> values = new List<string>() { post.Title, post.Body, post.Authors, post.AuthorIDs, post.Links, post.Active.ToString() };
                    Database.SaveToDatabase(fields, values, "Post");
                }
            }
        }

        /// <summary>
        /// Loads (async) from persistent database and recreates objects in volatile memory
        /// </summary>
        public static async Task LoadAsync()
        {
            List<object> x = await Database.GetAllFromDatabase("Post", false); // False removes writes to console. True by default.

            foreach (List<object> listItem in x)
            {

                string[] vs = listItem.Select(x => x.ToString()).ToArray();
                PostCRUD.CreatePost(vs);


            }
            
        }

        /// <summary>
        /// Updates post object based on user input.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="FormatException"></exception>
        public static void Update(int id)
        {
            Post target = Post.GetPost(id);
            Console.WriteLine(target.Title);
            Console.WriteLine("Please select the field you wish to edit (body, title, active)");
            string field = Console.ReadLine();

            Console.WriteLine("\nReplace with:");
            string newData = Console.ReadLine();

            Console.WriteLine();
            if (field != null && newData != null && target != null)
            {
                object p = field.ToLower() switch
                {
                    "body" => target.Body = newData,
                    "title" => target.Title = newData,
                    "active" => (newData.ToLower() == "true") ? target.Active = true : target.Active = false,
                    _ => throw new FormatException(),
                };

                target.EditDate = DateTime.Now;
            }

            string query =
                $"UPDATE Post " +
                $"SET {field.UpperFirstChar()} = '{newData}' " +
                $"WHERE ID = {id} ";
            Database.QueryDatabase(query);
        }

        

        public static void ErasePost()
        {
            Console.Clear();
            Console.WriteLine("Please designate which post you would like to erase:");
            Database.GetAllFromDatabase("Post");
            int uInput = int.Parse(Console.ReadLine());

            string query = $"DELETE FROM Post WHERE ID={uInput}";

            try
            {
                Database.QueryDatabase(query);
            }
            catch (UnableToDelete ex)
            { Console.WriteLine(ex.Message); }

        }
    }
}
