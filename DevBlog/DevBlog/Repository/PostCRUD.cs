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
                    p.Active = args[6] == "true" ? true : false;
                    Console.WriteLine(p.AuthorIDs);
                    Author a = Author.GetAuthorByID(int.Parse(p.AuthorIDs));
                    a.PostCount++;
                }
                if (args.Length == 3)
                {
                    Author a = Author.GetAuthorByID(int.Parse(args[0]));
                    p.Body = args[1];
                    p.Title = args[2];
                    p.Authors += a.Name;
                    p.AuthorIDs += a.ID;
                    p.Active = true;
                    a.PostCount++;

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
        internal static void SaveToBoth(Post a)
        {
            Post.PostDB.Add(a);
            Save();
        }

        /// <summary>
        /// Method for UI-based post creation, which ultimately calls the CreatePost method.
        /// </summary>
        public static void PostMessage()
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
        /// Reads all Posts currently stored in volatile storage (ASYNC).
        /// </summary>
        public static void ReadAll()
        {
            Console.WriteLine("\n Reading all Posts: ");

            foreach (Post post in Post.PostDB)
            {
                Console.WriteLine("########################");
                Console.WriteLine(post.ToString());
            }

        }

        /// <summary>
        /// Reads all posts currently stored in volatile storage (sync)
        /// </summary>
        public static async void ReadAllAsync()
        {
            Console.WriteLine("\n Reading all Posts: ");

            foreach (Post post in Post.PostDB)
            {
                Console.WriteLine("########################");
                Console.WriteLine(post.ToString());
            }

        }

        /// <summary>
        /// Saves Posts to permanent storage.
        /// </summary>
        public static void Save()
        {
            List<string> currentList = DatabaseHelper.GetColumn("Post", "ID");

            foreach (Post post in Post.PostDB)
            {
                Console.WriteLine(post.ToString());
                if (currentList.Count == 0 || !currentList.Contains((post.ID).ToString()))
                {
                    List<string> fields = new List<string>() { "Title", "Body", "Author", "AuthorID", "Links", "Active" };
                    List<string> values = new List<string>() { post.Title, post.Body, post.Authors, post.AuthorIDs, post.Links, post.Active.ToString() };
                    Database.SaveToDatabase(fields, values, "Post");
                }
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Loads (async).. WIP
        /// </summary>
        public static async void LoadAsync()
        {
            List<object> x = await Database.GetAllFromDatabase("Post"); // False removes writes to console. True by default.

            foreach (List<object> listItem in x)
            {

                string[] vs = listItem.Select(x => x.ToString()).ToArray();
                PostCRUD.CreatePost(vs);


            }
            Console.ReadKey();
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

        /// <summary>
        /// Helper-method used to update record links.
        /// </summary>
        /// <param name="target"></param>
        private static void UpdateRecordLinks(Post target)
        {
            string query =
                $"UPDATE Post " +
                $"SET [Links] = '{target.Links}' " +
                $"WHERE ID = {target.ID} ";
            Database.QueryDatabase(query);
        }

        /// <summary>
        /// Adds a specified URL to the post method.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="url"></param>
        public static void AddURL(Post target, string url)
        {
            bool addUrl = true;
            while (addUrl)
            {
                if (!string.IsNullOrEmpty(url))
                {
                    if (target.Links == "")
                    {
                        target.Links += url;
                    }
                    else if (target.Links != "")
                    {
                        target.Links += $";{url}";
                    }
                    Console.WriteLine("Do you wish to add another URL? [y/n]");
                    string input = Console.ReadLine();

                    if (input.ToLower() != "y")
                    { addUrl = false; }
                }
            }

            UpdateRecordLinks(target);
        }

        /// <summary>
        /// Removes URL form specified Post
        /// </summary>
        /// <param name="target"></param>
        /// <param name="uInput"></param>
        public static void RemoveURL(Post target, int uInput)
        {
            if (!target.Links.Contains(";") && uInput == 1)
            {
                target.Links = "";
            }
            else
            {
                if (uInput != target.Links.Split(";").Length)
                {
                    target.Links.Replace($"{target.Links.Split(";")[uInput]};", "");
                }
                else
                {
                    target.Links.Replace($";{target.Links.Split(";")[uInput]}", "");
                }
            }

            UpdateRecordLinks(target);
        }

        /// <summary>
        /// Method used to open URL
        /// </summary>
        /// <param name="id"></param>
        public static void OpenURL(int id)
        {
            Post p = Post.GetPost(id);

            if (p.Links.Split(";").Length > 1)
            {
                Console.WriteLine("Please select the URL you wish to open:\n");
                for (int i = 0; i < p.Links.Split(";").Length; i++)
                {
                    Console.WriteLine($"{i}: {p.Links.Split(";")[i]}");
                }
                int input = int.Parse(Console.ReadLine());

                p.Links.Split(";")[input].OpenGivenUrl();
            }
            else if (p.Links.Split(";").Length == 1) { p.Links.OpenGivenUrl(); }



        }
    }
}
