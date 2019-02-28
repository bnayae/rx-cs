using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace _07_Publish_Multi
{
    class Program
    {
        static void Main(string[] args)
        {
            var scd = TaskPoolScheduler.Default;
            var rnd = new Random();
            var xs = Observable.Interval(TimeSpan.FromMilliseconds(500))
                               //.Select(v => rnd.Next(0, 20))
                               .Publish()
                               .RefCount()
                               .ObserveOn(scd);

            var d1 = xs.Subscribe(v =>
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(3000);
                Console.WriteLine(".");
            });// Console.WriteLine(v));
            var d2 = xs.Subscribe(v => Console.WriteLine(Thread.CurrentThread.ManagedThreadId));// Console.WriteLine($"\t{v}"));

            //Thread.Sleep(2000);
            //d1.Dispose();
            //Console.WriteLine("A");
            //var d3 = xs.Subscribe(v => Console.WriteLine(Thread.CurrentThread.ManagedThreadId));// Console.WriteLine($"\t\t{v}"));
            //Thread.Sleep(2000);
            //d2.Dispose();
            //d3.Dispose();
            //Console.WriteLine("B");
            //var d4 = xs.Subscribe(v => Console.WriteLine(Thread.CurrentThread.ManagedThreadId));// Console.WriteLine($"\t\t\t{v}"));

            Console.ReadKey();
        }
    }
}
