using System;
using System.Reactive.Linq;

namespace _08_Subjects
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var rnd = new Random();
            var xs = Observable.Interval(TimeSpan.FromMilliseconds(1500))
                            .Select(v => (long)rnd.Next(0, 20))
                            .MyPublish();

            xs.Subscribe(v => Console.WriteLine(v));
            xs.Subscribe(v => Console.WriteLine($"\t{v}"));

            Console.ReadKey();


        }
    }
}
