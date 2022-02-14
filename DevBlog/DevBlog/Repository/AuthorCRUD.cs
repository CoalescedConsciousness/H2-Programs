using Common;

namespace DevBlog.Repository
{
    public class AuthorCRUD
    {
        /// <summary>
        /// Method to create an Author, which also stores it in the current list of objects (local storage) and SQL database (external storage)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Author CreateAuthor(string[] args)
        {
            Author author = new Author();
         
            
            if (args.Length == 5)
            {
                author.ID = int.Parse(args[0]);
                author.Name = args[1];
                author.Email = args[2];
                author.PostCount = int.Parse(args[3]);
                author.Active = bool.Parse(args[4]);
            }
            if (args.Length == 2)
            {
                author.ID = DatabaseHelper.GetNextID("Author");
                author.Name = args[0];
                author.Email = args[1];
            }
            if (args.Length == 0)
            {
                author.ID = DatabaseHelper.GetNextID("Author");
                author.Active = true;
                author.PostCount = 0;
            }

            Author.AuthorDB.Add(author);
            Save();
            return author;

        }

        /// <summary>
        /// Reads all Author objects in current local storage.
        /// </summary>
        public static async void ReadAllAsync()
        {
            foreach (Author a in Author.AuthorDB)
            {
                Console.WriteLine("################################");
                Console.WriteLine(a.ToString());
            }
        }

        /// <summary>
        /// Saves all Author objects to "external" permanent storage
        /// </summary>
        public static void Save()
        {
            List<string> currentList = DatabaseHelper.GetColumn("Author", "ID");
            foreach (Author author in Author.AuthorDB)
            {
                if (!currentList.Contains(author.ID.ToString()))
                {
                    List<string> fields = new List<string>() { "Name", "Email", "PostCount", "Active" };
                    List<string> values = new List<string>() { author.Name, author.Email, author.PostCount.ToString(), author.Active.ToString() };

                    Database.SaveToDatabase(fields, values, "Author");
                }
            }
        }

        /// <summary>
        /// WIP
        /// </summary>
        public static async Task LoadAsync()
        {

            List<object> x = await Database.GetAllFromDatabase("Author"); // False removes writes to console. True by default.

            foreach (List<object> listItem in x)
            {
                string[] vs = listItem.Select(x => x.ToString()).ToArray();
                AuthorCRUD.CreateAuthor(vs);
                
            }
            
        }

        /// <summary>
        /// Method for updating an Author object.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="FormatException"></exception>
        public static void Update(int id)
        {
            Author target = Author.AuthorDB.Find(x => x.ID == id);
            Console.WriteLine(target.Name);
            Console.WriteLine("Please select the field you wish to edit (name, email, active)");
            string field = Console.ReadLine();

            Console.WriteLine("\nReplace with:");
            string newData = Console.ReadLine();

            Console.WriteLine();
            if (field != null && newData != null && target != null)
            {
                object p = field.ToLower() switch
                {
                    "name" => target.Name = newData,
                    "email" => target.Email = newData,
                    "active" => (newData.ToLower() == "true") ? target.Active = true : target.Active = false,
                    _ => throw new FormatException(),
                };
            }

            string query = $"SELECT * FROM POST WHERE ID = {id} " +
                $"SET {field.UpperFirstChar()} = {newData}";
            Database.QueryDatabase(query);
        }

    }
}
