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
        
    }
}