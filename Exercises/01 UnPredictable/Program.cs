using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace _01_UnPredictable
{
    class Program
    {
        static void Main(string[] args)
        {
            var xs = Observable.Range(0, 10, NewThreadScheduler.Default);
            //var xs = Observable.Interval(TimeSpan.FromMilliseconds(20));
            //var ys = xs.TakeUntil(xs.Where(i => i == 5));
            //var ps = xs.Publish().RefCount();
            //var ys = ps.TakeUntil(ps.Where(i => i == 5));
            var ys = xs.Publish(hot => hot.TakeUntil(hot.Where(i => i == 5)));
            ys.Subscribe(v => Console.WriteLine(v));
            //ps.Connect();
            Console.ReadKey();
        }
    }
}
