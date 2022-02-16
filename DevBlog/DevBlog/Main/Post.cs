using Common;
using DevBlog.Repository;

namespace DevBlog
{
    [Serializable()]
    public class Post
    {
        internal const string PostDatabase = @"posts.txt";
        public static List<Post> PostDB = new List<Post>();

        string _title;
        string _body;
        string _authors;
        string _authorIDs;
        int _id;
        bool _active;

        public Post()
        {
            Date = DateTime.Now;
            ID = DatabaseHelper.GetNextID("Post");
            Active = true;
            Links = "";
        }
       

        public string Links { get; set; }
        
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
        public string Authors
        {
            get => _authors;
            set => _authors = value;   
        }
        public string AuthorIDs
        {
            get => _authorIDs;
            set => _authorIDs = value;
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

        /// <summary>
        /// Fetches post by its ID
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        internal static Post GetPost(int i)
        {
            return Post.PostDB.Find(x => x.ID == i);
        }

        /// <summary>
        /// Override of ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string links = default;
            if (Links.Split(";").Length > 1)
            {
                for (int i = 0; i < Links.Split(";").Length; i++)
                {
                    links += $"\n          {i+1}- {Links.Split(";")[i]}";
                }
            }
            else
            { links = ""; }
            
            string result =
                $"-- Title: {Title}\n" +
                $"-- Body: {Body}\n" +
                $"-- Authors: {Authors}\n" +
                $"-- Author IDs: {AuthorIDs}\n" +
                $"-- Links: {links}\n" +
                $"-- Active: {Active}\n";


            return result;
        }

        /// <summary>
        /// Reads all Posts currently stored in volatile storage.
        /// </summary>
        public static void ReadAll()
        {
            Console.Clear();
            Console.WriteLine("Reading all Posts: \n");

            foreach (Post post in Post.PostDB)
            {
                if (post.Active == true)
                {
                    Console.WriteLine("########################");
                    Console.WriteLine(post.ToString());
                }
            }
            Console.ReadKey();
        }


    }
}
