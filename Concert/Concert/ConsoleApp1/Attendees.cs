using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concert
{
    public class Attendees
    {
        public string FName { get; set; }
        public string SName { get; set; }
        private string Email { get; set; }

        public int AttID { get; }

        public string FullName { 
            get 
            {
                return _fullname;
            } 
        }  

        private readonly string _fullname;
                

        public Attendees(string fName, string sName)
        {
            this.FName = fName;
            this.SName = sName;

            _fullname = $"{fName} {sName}";

            this.AttID = Globals.AccID;
            Globals.AccID++;
            
            
        }
    }
}
