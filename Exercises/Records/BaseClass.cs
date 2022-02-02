using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Records
{
    public class BaseClass
    {
        public int Prop { get; set; }
        public BaseClass()
        {

        }
        public BaseClass(int Prop)
        {
            this.Prop = Prop;
        }
    }
}