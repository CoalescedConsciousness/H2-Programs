using Common;
using Storage;

namespace DevBlog
{
    [Serializable()]
    public record Post
    {
        string _title;
        string _body;
        List<Author> _authors;
        int _id;

        public Post(Author writer, string text, string title)
        {
            Title = title;
            Authors.Add(writer);
            Body = text;
            Date = DateTime.Now;
            ID = IDHandler.SetPostID();
            Active = true;
        }

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
        }

        public int ID
        {
            get => _id;
            set => _id = value; 
        }

        public bool Active
        {
            get => Active;
            set => Active = value;
        }
        
        public void PostMessage(Author writer, string text, string title)
        {
            PostStorage.PostDB.Add(new Post(writer, text, title));
        }
    }
}