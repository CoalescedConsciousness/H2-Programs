using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevBlog
{
    public interface IStorage
    {
        void Load();
        void Save();
    }
}