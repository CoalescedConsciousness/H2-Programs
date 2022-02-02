using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevBlog
{
    public class PostStorage : IStorage
    {
        public Post Post
        {
            get => default;
            set
            {
            }
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Load()
        {
            throw new System.NotImplementedException();
        }

        public void Create()
        {
            throw new System.NotImplementedException();
        }
    }

    public class AuthorStorage : IStorage
    {
        public Author Author
        {
            get => default;
            set
            {
            }
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Load()
        {
            throw new System.NotImplementedException();
        }
    }
}