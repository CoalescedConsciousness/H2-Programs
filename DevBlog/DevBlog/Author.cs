using Common;
using Storage;

namespace DevBlog
{
    [Serializable]
    public class Author
    {

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

        // Constructor
        private Author(string name, string email)
        {
            Name = name;
            Email = email;
        }

        private Author() { }

        // Methods

        /// <summary> 
        /// Creates an (anonymous) Author object and adds it to a (known) list.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        public static Author CreateAuthor(string name, string email)
        { 
            Author author = new Author(name, email);
            // Depricated: AuthorStorage.AuthorDB.Add(author);
            author.ID = Database.GetNextID("Author");
            author.Active = true;
            author.PostCount = 0;
            AuthorStorage.AuthorDB.Add(author);
            AuthorStorage.Save();
            return author;
        
        }

        public static Author GetAuthorByID(string id)
        {
            return AuthorStorage.AuthorDB.Find(x => x.ID == int.Parse(id));
        }
        public static Author GetAuthorByName(string name)
        {
            return AuthorStorage.AuthorDB.Find(x => x.Name == name);
        }

    }
}