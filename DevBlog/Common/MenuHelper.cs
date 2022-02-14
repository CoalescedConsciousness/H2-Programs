using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MenuHelper
    {
        public delegate void Selection();

        static public void GetSelection(string input, string target, Selection choice)
        {
            if (input.ToLower() == target.ToLower()) { choice(); return; }
        }
    }
}
