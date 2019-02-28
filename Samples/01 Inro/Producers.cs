using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _01_Inro
{
    internal static class Producers
    {
        public static IObservable<int> Sync()
        {
            return Observable.Create<int>(observer =>
            {
                var d = new BooleanDisposable();
                for (int i = 0; i < 10; i++)
                {
                    if (d.IsDisposed)
                        return Disposable.Empty; 

                    observer.OnNext(i);
                    Thread.Sleep(100 * i);
                }
                observer.OnCompleted();
                observer.OnNext(100); // wrong implementation
                return d;
            });
        }
        public static IObservable<int> Async()
        {
            return Observable.Create<int>(async (observer, ct) =>
            {
                for (int i = 0; i < 10; i++)
                {
                    if (ct.IsCancellationRequested)
                        return; 

                    await Task.Delay(100 * i).ConfigureAwait(false);
                    observer.OnNext(i);
                }
                observer.OnCompleted();
                observer.OnNext(100); // wrong implementation
            });
        }
        public static IObservable<int> AsyncFast()
        {
            return Observable.Create<int>((observer) =>
            {
                for (int i = 0; i < 10; i++)
                {
                    observer.OnNext(i);
                }
                return Disposable.Empty;
            });
        }
    }
}
