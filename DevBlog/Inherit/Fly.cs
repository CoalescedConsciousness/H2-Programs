using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inherit
{
    public record Fly : Transportmiddel
    {
        public Fly(string navn, int hoejde, int topfart)
        {
            Navn = navn;
            MaksHoejde = hoejde;
        }

        public Fly(string navn, int hoejde)
        {
            Navn = navn;
            MaksHoejde = hoejde;
        }
        public Fly()
        { }

        string _navn;
        public string Navn { 
            get 
            {
                return _navn ?? "I Can fly!";
            } 
            set 
            {
                _navn = value;
            }
        }

        public int MaksHoejde { get; set; }

        public override string Start()
        {
            return "Kom på vingerne!";
        }
    }
}