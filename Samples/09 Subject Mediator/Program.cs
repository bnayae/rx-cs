using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _09_Subject_Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start");
            var m = new Mediator<int>();

            //Task.Run(async () =>
            //{
            //    int i = 0;
            //    while (true)
            //    {
            //        await Task.Delay(300);
            //        m.Target.OnNext(i++);
            //    }

            //});
            Observable.Interval(TimeSpan.FromMilliseconds(300))
                .Select(v => (int)v)
                        .Subscribe(m.Target);

            Thread.Sleep(1000);
            m.Source.Subscribe(v => Console.WriteLine(v));


            Thread.Sleep(3000);
            m.Source.Subscribe(v => Console.WriteLine ($"\t{v}"));

            Console.ReadKey();

        }
    }
}
