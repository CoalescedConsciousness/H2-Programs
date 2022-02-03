using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevBlog
{
    public interface IStorage
    {
        public void Load();
        public void Save();
    }
}