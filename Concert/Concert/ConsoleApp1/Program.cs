using System;
using System.Collections.Generic;
using System.Linq;


namespace Concert
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("First name:");
            string fInput = Console.ReadLine();
            Console.WriteLine("Last name:");
            string lInput = Console.ReadLine();

            Attendees attendee = new Attendees(fInput, lInput);
            Console.WriteLine($"{attendee.AttID} || {attendee.FName}, {attendee.SName}, {attendee.FullName}");
            Attendees sAtt = new Attendees(fInput, lInput);
            Console.WriteLine($"{sAtt.AttID} || {sAtt.FName}, {sAtt.SName}, {sAtt.FullName}");
            Console.ReadKey();
        }
    }
}