using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _01_Inro
{
    class Producer : IObservable<int>
    {
        public IDisposable Subscribe(IObserver<int> observer)
        {
            var d = new BooleanDisposable();
            Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    if (d.IsDisposed)
                        return;

                    observer.OnNext(i);
                    Thread.Sleep(1000);
                }
                observer.OnCompleted();
            });

            return d;
        }
    }
}
