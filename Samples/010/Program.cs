using System;
using System.Reactive.Linq;
using System.Threading;

namespace _010
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            var xs = Observable.Interval(TimeSpan.FromSeconds(0.1))
                                .Replay(5).RefCount();
            xs.Subscribe(v => Console.WriteLine(v));
            Thread.Sleep(2000);
            xs.Subscribe(v => Console.WriteLine($"\t{v}"));
            Console.ReadKey();

        }
    }
}
