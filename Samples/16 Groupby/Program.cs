using System;
using System.Reactive.Linq;

namespace _16_Groupby
{
    class Program
    {
        static void Main(string[] args)
        {
            var xs = Observable.Interval(TimeSpan.FromSeconds(0.5))
                      .Select(v => {
                          if (v % 10 == 0)
                              return 1;
                          if (v % 5 == 0)
                              return 2;
                          return 3;
                      }).Publish().RefCount();
            //var gs = xs.GroupBy(m => m % 3);
            var gs = xs.GroupByUntil(m => m % 3, m => m, g => g.Throttle(TimeSpan.FromSeconds(2))); //Observable.Timer(TimeSpan.FromSeconds(4)));
            gs.Subscribe(g =>
            {
                Console.WriteLine($"Key = {g.Key}");
                g.Subscribe(v => Console.WriteLine($"\t{g.Key}: {v}"));
            });

            Console.ReadKey();
        }
    }
}
