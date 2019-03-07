using System;
using System.Reactive.Linq;

namespace _13_Custom_Aggregation
{
    class Program
    {
        static void Main(string[] args)
        {

            var xs = Observable.Interval(TimeSpan.FromMilliseconds(100));
            var sums = from w in xs.Window(TimeSpan.FromSeconds(0.5))
                       from result in w.Aggregate(string.Empty, (acc, val) => $"{acc}, {val}")
                       select result;
            sums.Subscribe(m => Console.WriteLine(m));

            Console.ReadKey();


        }
    }
}
