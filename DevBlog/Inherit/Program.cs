using System;
using System.Collections.Generic;
using System.Linq;
using Inherit;

namespace Program;

public record Program
{
    static void Main(string[] args)
    {
        Fly nFly = new("D'oh", 20);
        Bil nBil = new(4);
        Cykel nCykel = new(32);
        nCykel.Topfart = 10;

        Fly sFly = new("", 30);
        Fly aFly = new();
        aFly.Topfart = 10000;

        Console.WriteLine(nFly.ToString());
        Console.WriteLine(nBil.ToString());
        Console.WriteLine(nCykel.ToString());
        Console.WriteLine(sFly.ToString());
        Console.WriteLine(aFly.ToString());
        Console.WriteLine();
        Console.WriteLine(nFly.Start());
        Console.WriteLine(nBil.Start());
        Console.WriteLine(nCykel.Start());        
        
        Console.ReadKey();
    }
}