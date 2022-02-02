using Records;

namespace Program;

public record Program
{
    static void Main()
    {
        DerivedClass bc = new();
        DerivedClass dc = new();
        DerivedRecord br = new();
        DerivedRecord dr = new();

        Console.WriteLine("Base == Derived");
        Console.WriteLine(bc == dc);
        Console.WriteLine(br == dr);
        Console.WriteLine("ToString: {bc, dc, br, dr}");
        Console.WriteLine(bc.ToString());
        Console.WriteLine(dc.ToString());
        Console.WriteLine(br.ToString());
        Console.WriteLine(dr.ToString());
        Console.WriteLine();

        
        DerivedClass bc2 = new(1);
        DerivedClass dc2 = new(1);
        // DerivedRecord br2 = new(1);
        // DerivedRecord dr2 = new(1);
        
        DerivedRecord br2 = br with { Prop = 1 }; 
        DerivedRecord dr2 = dr with { Prop = 1, Test = 2 };

        Console.WriteLine("First = 1 (==) Second = 1");
        Console.WriteLine(bc2 == dc2);
        Console.WriteLine(br2 == dr2);
        Console.WriteLine("ToString: {bc, dc, br, dr}");
        Console.WriteLine(bc2.ToString());
        Console.WriteLine(dc2.ToString());
        Console.WriteLine(br2.ToString());
        Console.WriteLine(dr2.ToString());
        Console.WriteLine();

        DerivedClass bc3 = new(1);
        DerivedClass dc3 = new(2);
        DerivedRecord br3 = br2 with { Prop = 1 };
        DerivedRecord dr3 = dr2 with { Prop = 2 };

        Console.WriteLine("First = 1 (==) Second = 2");
        Console.WriteLine(bc3 == dc3);
        Console.WriteLine(br3 == dr3);
        Console.WriteLine("ToString: {bc, dc, br, dr}");
        Console.WriteLine(bc3.ToString());
        Console.WriteLine(dc3.ToString());
        Console.WriteLine(br3.ToString());
        Console.WriteLine(dr3.ToString());
        Console.WriteLine();

        Console.ReadKey();
    }
}