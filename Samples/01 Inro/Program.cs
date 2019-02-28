using System;
using System.Threading;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace _01_Inro
{
    class Program
    {
        static void Main(string[] args)
        {
            //var p = new Producer();
            //var p = Producers.Sync();
            var p = Producers.Async();
            //var p = Producers.AsyncFast();
            var c = new Subscriber();

            IDisposable subscription = p.Do(v => Trace.WriteLine($"->{v}"))
                                        .Throttle(TimeSpan.FromMilliseconds(320))
                                        .Do(v => Trace.WriteLine($"{v}->"))
                                        .Where(v => v % 2 == 0)
                                        .Select(v => v * 2)
                                        //.Select(async (v, i) =>
                                        //{
                                        //    await Task.Delay(TimeSpan.FromSeconds(v % 3)).ConfigureAwait(false);
                                        //    return new string('*', v + 1);
                                        //})
                                        //.Subscribe(async t =>
                                        //            {
                                        //                var v = await t;
                                        //                Console.WriteLine($"{v}-");
                                        //            },
                                        //           e => Console.WriteLine("X"),
                                        //           () => Console.WriteLine("|"));
                                        .Subscribe(c);

            Console.WriteLine("Continue");
            Thread.Sleep(3200);
            //subscription.Dispose();
            Console.ReadKey();
        }
    }
}
