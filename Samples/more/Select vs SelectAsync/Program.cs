using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Select_vs_SelectAsync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TraditionalRx();
            Console.ReadKey();
        }

        private static void TraditionalRx()
        {
            var xs = Observable.Range(1, 15)
                               .Select(SelectAsync)
                               .SelectMany(m => Observable.FromAsync<string>(() => m));
            xs.Subscribe(m => Console.WriteLine(m));

        }

        //private static void AsyncRx()
        //{
        //    var xs = AsyncObservable.Range(1, 15)
        //                       .Select(SelectAsync);
        //    xs.Subscribe(m => Console.WriteLine(m));

        //}

        private static async Task<string> SelectAsync(int i)
        {
            await Task.Delay((i % 3) * 500).ConfigureAwait(false);
            return new string('*', i);
        }


    }
}
