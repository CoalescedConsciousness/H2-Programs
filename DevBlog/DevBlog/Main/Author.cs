using Common;

namespace DevBlog
{
    [Serializable]
    public class Author
    {
        internal static string AuthorDatabase = @"authors.txt";
        public static List<Author> AuthorDB = new List<Author>();

        string _name;
        string _email;
        int _id;
        int _postCount;
        bool _active;
        
        public List<Post> Posts { get; set; }
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public string Email
        {
            get => _email;
            set => _email = value;
        }

        

        public int ID
        {
            get => _id;
            set => _id = value;
        }

        public int PostCount
        {
            get => _postCount;
            set => _postCount = value;
        }

        public bool Active
        {
            get => _active;
            set => _active = value;
        }


       
        // Method
        

        /// <summary>
        /// Fetches author object from list by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Author GetAuthorByID(int id)
        {
            return AuthorDB.Find(x => x.ID == id);
        }


        /// <summary>
        /// Fetches author object from list by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Author GetAuthorByName(string name)
        {
            return AuthorDB.Find(x => x.Name == name);
        }

        public override string ToString()
        {
            string result =
                $"-- Name: {Name}\n" +
                $"-- Email: {Email}\n" +
                $"-- Post Count: {PostCount}\n" +
                $"-- Active: {Active}\n";
            return result;
        }

        /// <summary>
        /// Reads all Author objects in current local storage.
        /// </summary>
        public static void ReadAll()
        {
            Console.Clear();
            foreach (Author a in Author.AuthorDB)
            {
                if (a.Active == true)
                {
                    Console.WriteLine("################################");
                    Console.WriteLine(a.ToString());
                }
            }
            Console.ReadLine();
        }

        public static void ToggleAuthor()
        {
            Console.Clear();
            Console.WriteLine("Please designate which author (ID) you would like to de/activate:\n");
            Database.GetAllFromDatabase("Author");
            int uInput = int.Parse(Console.ReadLine());

            int state = default;

            if (AuthorDB.First(x => x.ID == uInput).Active == true)
            {
                state = 0;
                AuthorDB.First(x => x.ID == uInput).Active = false;
            }
            else if (AuthorDB.First(x => x.ID == uInput).Active == false)
            {
                state = 1;
                AuthorDB.First(x => x.ID == uInput).Active = true;
            }
            
            string query = $"UPDATE Author SET Active = {state} WHERE ID = {uInput}";

            Database.QueryDatabase(query);
            

        }
    }
}