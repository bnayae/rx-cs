using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace _12_Windows___Aggregate
{
    class Program
    {
        static void Main(string[] args)
        {
            //IEnumerable<IEnumerable<int>> es =
            //    Enumerable.Range(0, 100).Select(m => Enumerable.Range(m, m + 10));
            //var sums = es.Select(m => m.Sum());


            var xs = Observable.Never<int>();//Observable.Interval(TimeSpan.FromMilliseconds(30));
            //var ws = xs.Window(TimeSpan.FromSeconds(1));
            //IObservable<long> sums = ws.SelectMany(m => m.LastAsync());
            var sums = from w in xs.Window(TimeSpan.FromSeconds(1))
                       let safeWin = w.Select(m => (int?)m).DefaultIfEmpty()
                       from ohlc in Observable.Zip(
                                        safeWin.FirstAsync(),
                                        safeWin.LastAsync(),
                                        safeWin.Min(),
                                        safeWin.Max())
                       select new { First = ohlc[0], Last = ohlc[1], Min = ohlc[2], Max = ohlc[3]};
            // NOT REALY THE SOLUTION: var sums = ws.Select(m => Tuple.Create(m.Max(), m.Min()));

            sums.Subscribe(m => Console.WriteLine(m));
            //ws.Subscribe(w =>
            //{
            //    w.Subscribe(m => Console.WriteLine());
            //});

            Console.ReadKey();
        }
    }
}
