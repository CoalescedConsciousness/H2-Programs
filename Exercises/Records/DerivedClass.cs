using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Records
{
    public class DerivedClass : BaseClass
    {
        public int Prop { get; set; }
        public DerivedClass()
        {

        }
        public DerivedClass(int Prop) : base(Prop)
        {
            this.Prop = Prop;
        }
    }
}