using System;
using System.Reactive.Linq;

namespace _06_Hot_Cold_Pitfall
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random();
            var xs = Observable.Interval(TimeSpan.FromMilliseconds(100))
                               .Select(v => rnd.Next(0, 20));

            //Pitfall(xs);
            //Option1(xs);
            Option2(xs);
            Console.ReadKey();
        }

        private static void Pitfall(IObservable<int> xs)
        {
            var trigger = xs.Do(v => Console.Write($" [{v}] ")).Where(v => v == 10);
            var ys = xs.TakeUntil(trigger);
            ys.Subscribe(v => Console.Write($"{v},"));
        }

        private static void Option1(IObservable<int> xs)
        {
            var hot = xs.Publish();
            var trigger = hot.Do(v => Console.Write($" [{v}] ")).Where(v => v == 10);
            var ys = hot.TakeUntil(trigger);
            ys.Subscribe(v => Console.Write($"{v},"));
            IDisposable connection = hot.Connect();
        }

        private static void Option2(IObservable<int> xs)
        {
            var zs = xs.Publish(hot =>
            {
                var trigger = hot.Do(v => Console.Write($" [{v}] ")).Where(v => v == 10);
                var ys = hot.TakeUntil(trigger);
                return ys;
            });
            zs.Subscribe(v => Console.Write($"{v},"));
        }
    }
}
