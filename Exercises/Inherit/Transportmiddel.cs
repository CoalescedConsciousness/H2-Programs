using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inherit
{
    public abstract record Transportmiddel
    {
        public int Topfart { get; set; }

        public Transportmiddel(int fart)
        {
            Topfart = fart;
        }
        public Transportmiddel()
        {
        }

        public abstract string Start();
    }
}