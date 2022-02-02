using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Records
{
    public record BaseRecord
    {
        public int Prop { get; set; }
        public int Test { get; set; }

        public BaseRecord()
        {

        }
        public BaseRecord(int Prop)
        {
            this.Prop = Prop;
        }
    }
}