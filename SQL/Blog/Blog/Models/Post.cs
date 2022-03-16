using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Post
    {
        private string _id;
        private string _title;
        private string _body;
        private DateTime? _createDate;
        private DateTime? _editDate;
        private Contacts _author;

        public string Id { get { return _id; } set { _id = value; } }

        [DisplayFormat(NullDisplayText = "No Title")]
        public string Title { get { return _title; } set { _title = value; } }
        public string Body { get { return _body; } set { _body = value; } }
        public DateTime? CreateDate { get { return _createDate; } set { _createDate = value;} }
        public DateTime? EditDate { get { return _editDate; } set { _editDate = value;} }
        public Contacts Author { get { return _author; } set { _author = value; } }

        public Post() { }

        public Post(string title, string body, Contacts author)
        {
            Title = title;
            Body = body;
            Author = author;
        }
    }
}
