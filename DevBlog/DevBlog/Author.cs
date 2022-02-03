using Common;
using Storage;

namespace DevBlog
{
    public record Author
    {

        string _name;
        string _email;
        int _id;
        int _postCount;

        
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
            get => Active;
            set => Active = value;
        }

        // Constructor
        private Author(string name, string email)
        {
            Name = name;
            Email = email;
            ID = IDHandler.SetAuthorID();
        }

        // Methods

        /// <summary>
        /// Creates an (anonymous) Author object and adds it to a (known) list.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        public void CreateAuthor(string name, string email)
        { AuthorStorage.AuthorDB.Add(new Author(name, email)); }


    }
}