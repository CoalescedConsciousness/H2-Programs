using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MenuHelper
    {
        // Delegate + Async Delegate
        public delegate void Selection();

        public delegate Task SelectionAsync();

        // And their functions.
        static public async Task GetSelectionAsync(string input, string target, SelectionAsync choice)
        {
            if (input.ToLower() == target.ToLower()) { await choice(); return; }
        }

        static public void GetSelection(string input, string target, Selection choice)
        {
            if (input.ToLower() == target.ToLower()) { choice(); return; }
        }
    }
}
