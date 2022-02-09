using Common;
using Storage;

namespace DevBlog
{
    [Serializable()]
    public class Post
    {
        string _title;
        string _body;
        List<Author> _authors;
        int _id;
        bool _active;

        public Post(Author writer, string text, string title)
        {
            List<Author> authors = new List<Author>();
            authors.Add(writer);
            Title = title;
            Body = text;
            Date = DateTime.Now;
            ID = Database.GetNextID("Post");
            Active = true;
            PostStorage.PostDB.Add(this);
            PostStorage.Save();
        }
        public Post() { }

        public List<string> Links { get; set; }
        

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public string Body
        {
            get => _body;
            set => _body = value;
        }

        public DateTime Date { get; set; }
        public DateTime EditDate { get; set; }
        public List<Author> Authors
        {
            get => _authors;
            set => _authors = value;   
        }

        public int ID
        {
            get => _id;
            set => _id = value; 
        }

        public bool Active
        {
            get => _active;
            set => _active = value;
        }
        
        
        public static void PostMessage()
        {
            Console.WriteLine("Please designate which Author is writing:");
            List<string> ids = Database.GetColumn("Author");
            List<string> names = Database.GetColumn("Author", "Name");

            var ts = ids.Zip(names);
            foreach (var x in ts)
            {
                Console.WriteLine($"{x.First} {x.Second}");
            }
            string aInput = Console.ReadLine();
            Author author = Author.GetAuthorByID(aInput);
            Console.WriteLine("Title of your post: ");
            string tInput = Console.ReadLine();
            Console.WriteLine("Write the main body of your post:");
            string bInput = Console.ReadLine();

            Post p = new Post(author, bInput, tInput);
            PostStorage.PostDB.Add(p);
            PostStorage.Save();

        }
    }
}