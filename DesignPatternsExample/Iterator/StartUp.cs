using IteratorPattern.Aggregators;
using IteratorPattern.Iterators;
using System;

namespace IteratorPattern
{
    public class StartUp
    {
        static void Main()
        {
            ConcreteAggregate agr = new ConcreteAggregate();
            agr[0] = "Item 1";
            agr[1] = "Item 2";
            agr[2] = "Item 3";
            agr[3] = "Item 4";
            agr[4] = "Item 5";

            // Create Iterator and provide aggregate
            Iterator i = agr.CreateIterator();

            Console.WriteLine("Iterating over collection:");

            object item = i.First();
            while (item != null)
            {
                Console.WriteLine(item);
                item = i.Next();
            }
        }
    }
}