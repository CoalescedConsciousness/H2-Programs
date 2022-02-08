using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevBlog
{
    public interface IStorage
    {
        public static void Load()
        { }
        public static void Save()
        { }
        public static void Update()
        { }
        public static void ReadAll()
        { }

    }
}