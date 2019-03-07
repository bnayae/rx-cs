using System;
using System.Reactive.Linq;

namespace _14_Scan
{
    class Program
    {
        static void Main(string[] args)
        {
            var xs = Observable.Interval(TimeSpan.FromSeconds(1));
            var ys = xs.Do(m => Console.Write($"{m}: "))
                       .Scan((acc, cur) => cur % 2 == 0 ? acc + cur : acc - cur);
            ys.Subscribe(m => Console.WriteLine(m));

            Console.ReadLine();

        }
    }
}
