using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inherit
{
    public record Cykel : Transportmiddel
    {
        public Cykel(int gear)
        {
            AntalGear = gear;
        }

        public int AntalGear { get; set; }

        public override string Start()
        {
            return "Træd i pedalerne!";
        }
    }
}