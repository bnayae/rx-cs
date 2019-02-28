using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace _05_Hot_vs_Cold
{
    class ColdProducer : IObservable<int>
    {
        public IDisposable Subscribe(IObserver<int> observer)
        {
            var d = new BooleanDisposable();
            Task.Run(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(100).ConfigureAwait(false);
                    if (d.IsDisposed)
                        return;
                    observer.OnNext(i);
                }
                observer.OnCompleted();
            });
            return d;
        }
    }
}
