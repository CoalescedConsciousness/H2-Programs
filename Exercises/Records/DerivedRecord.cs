using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Records
{
    public record DerivedRecord : BaseRecord
    {

        public int Prop { get; set; }
        public int Test { get; set; }
        public DerivedRecord()
        {

        }

        public DerivedRecord(int Prop) : base(Prop)
        {
            this.Prop = Prop;
        }
    }
}