using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

// TODO: offset 'false' by 0.5 second and ignore it if 'true' produce during the offset

namespace _02_Cond_Cancel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            var xs = Observable.Create<bool>(async (observer) =>
            {
                observer.OnNext(true);
                observer.OnNext(false);
                await Task.Delay(600).ConfigureAwait(false);
                observer.OnNext(true);
                observer.OnNext(false); // should be ignored
                await Task.Delay(100).ConfigureAwait(false);
                observer.OnNext(true);
                await Task.Delay(600).ConfigureAwait(false);
                observer.OnCompleted();
            });

            var zs = xs.Publish(hot =>
            {

                var ts = hot.Where(m => m);
                var fs = hot.Where(m => !m)
                            .Delay(TimeSpan.FromSeconds(0.5))
                            .TakeUntil(ts) // ignore when 'true' produce during the delay
                            .Repeat();

                return Observable.Merge(ts, fs);
            });

            zs.Subscribe(v => Console.WriteLine(v));
            Console.ReadKey();
        }
    }
}
