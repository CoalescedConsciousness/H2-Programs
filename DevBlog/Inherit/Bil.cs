using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inherit
{
    public record Bil : Transportmiddel
    {
        public Bil(int saeder)
        {
            AntalSaeder = saeder;
        }

        public int AntalSaeder { get; set; }

        public override string Start()
        {
            return "Vroom vroom, etc.";
        }
    }
}