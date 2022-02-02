using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevBlog
{
    public class Author
    {
        public string Name
        {
            get => default;
            set
            {
            }
        }

        public List<Post> Posts { get; set; }

        public int ID
        {
            get => default;
            set
            {
            }
        }

        public bool Active
        {
            get => default;
            set
            {
            }
        }
    }
}