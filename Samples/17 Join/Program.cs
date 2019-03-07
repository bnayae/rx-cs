using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace _17_Join
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var xs = Observable.Create<string>(async (o) =>
            {
                o.OnNext("A");
                await Task.Delay(1000).ConfigureAwait(false);
                o.OnNext("B");
                await Task.Delay(2000).ConfigureAwait(false);
                o.OnNext("C");
                await Task.Delay(1000).ConfigureAwait(false);
                o.OnNext("D");
            });
            var ys = Observable.Create<int>(async (o) =>
            {
                await Task.Delay(2000).ConfigureAwait(false);
                o.OnNext(1);
                o.OnNext(2);
                await Task.Delay(1300).ConfigureAwait(false);
                for (int i = 0; i < 7; i++)
                {
                    o.OnNext(i + 10);
                }

            });

            // x: a-----b--------c--
            //     =======|
            //          ========|
            //                    =======|
            // y: ------------1-----------
            //                 ======|
            //IObservable<(string X, int Y)> zs = Observable.Join(
            //                    xs,
            //                    ys,
            //                    x => Observable.Timer(TimeSpan.FromSeconds(1.2)),
            //                    y => Observable.Timer(TimeSpan.FromSeconds(1.2)), //Observable.Empty<int>(),
            //                    (x, y) => (X: x, Y: y));

            // x: a-----b--------c--------------
            //     =====|
            //          =========|
            //                    ==============
            // y: ------------1-----------
            //                |
            //IObservable<(string X, int Y)> zs = xs.Publish(hotX =>
            //{
            //    return Observable.Join(
            //                    hotX,
            //                    ys,
            //                    x => hotX, //Observable.Timer(TimeSpan.FromSeconds(1.2)),
            //                    y => Observable.Empty<int>(),
            //                    (x, y) => (X: x, Y: y));
            //                    });
            //zs.Subscribe(v => Console.WriteLine($"{v.X} : {v.Y}"));

            // x: a-----b--------c--------------
            //     =====|
            //          =========|
            //                    ==============
            // y: ------------1-----------
            //                |
            //IObservable<(string X, IObservable<int> Y)> zs = xs.Publish(hotX =>
            //var zs = xs.Publish(hotX =>
            //{
            //    return hotX.GroupJoin(
            //                    ys,
            //                    x => hotX, //Observable.Timer(TimeSpan.FromSeconds(1.2)),
            //                    y => Observable.Empty<int>(),
            //                    (x, y) => (X: x, Y: y));
            //});

            //zs.Subscribe(v =>
            //{
            //    Console.WriteLine(v.X);
            //    v.Y.Subscribe(m => Console.WriteLine($"\t{m}"));
            //});


            var zs = xs.Publish(hotX =>
            {
                return from data in hotX.GroupJoin(
                                ys,
                                x => hotX, //Observable.Timer(TimeSpan.FromSeconds(1.2)),
                                y => Observable.Empty<int>(),
                                (x, y) => (X: x, Y: y))
                       from count in data.Y.Count()
                       select (X: data.X, Count: count);
            });

            zs.Subscribe(v => Console.WriteLine($"{v.X} = {v.Count}"));

            Console.ReadKey();
        }
    }
}
