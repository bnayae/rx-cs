using System;
using System.Threading;
using System.Reactive.Linq;

namespace _05_Hot_vs_Cold
{
    class Program
    {
        static void Main(string[] args)
        {
            //var xs = new ColdProducer();
            var xs = new HotProducer();
            xs.Where(v => v % 2==0)
              .Subscribe(v => Console.WriteLine(v), () => Console.WriteLine("|"));
            Thread.Sleep(500);
            xs.Sample(TimeSpan.FromSeconds(0.5))
              .Subscribe(v => Console.WriteLine($"\t{v}"), () => Console.WriteLine("\t|"));

            Console.ReadKey();
        }
    }
}
