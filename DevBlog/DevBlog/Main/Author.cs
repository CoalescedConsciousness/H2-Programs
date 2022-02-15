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
        
        
    }
}