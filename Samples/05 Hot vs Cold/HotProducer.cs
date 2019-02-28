using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _05_Hot_vs_Cold
{
    class HotProducer : IObservable<int>
    {
        private int _i = 0;
        private readonly Timer _tmr;
        private readonly ConcurrentDictionary<Guid, IObserver<int>> _subscribers = new ConcurrentDictionary<Guid, IObserver<int>>();

        public HotProducer()
        {
            _tmr = new Timer(s =>
            {
                int val = Interlocked.Increment(ref _i);
                foreach (var obs in _subscribers.Values)
                {
                    obs.OnNext(val);
                }

            }, null, 100, 100);
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            var id = Guid.NewGuid();
            _subscribers.TryAdd(id, observer);
            return Disposable.Create(() => _subscribers.TryRemove(id, out var _));
        }
    }
}

