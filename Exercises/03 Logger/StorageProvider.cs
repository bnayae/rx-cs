using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading;

namespace _03_Logger
{
    public class StorageProvider
    {
        public StorageProvider(IObservable<LogMessage> stream)
        {

            // TODO: 
            // #2 ignore log when repeat 
            //stream = stream.DistinctUntilChanged(m => m.Level);
            //stream = stream.DistinctUntilChanged(LogComparer.LevelAndContent);

            // #1 Split Debug level (handle it on below normal thread priority)
            //    default handling for all other log level
            //var scd = new EventLoopScheduler(start =>
            //                    new Thread(start) { Priority = ThreadPriority.BelowNormal });

            //var ds = stream.Where(m => m.Level == LogLevel.Debug)
            //      .ObserveOn(scd);
            //var nds = stream.Where(m => m.Level != LogLevel.Debug);
            //var ls = Observable.Merge(ds, nds);
            //ls.Subscribe(v => Save(v.ToString()), ex => Save(ex.ToString()), () => Save("Complete !?"));

            // #3 when log (of specific type) repeat multiple time in 1 second
            //    write the count and content of the last repetition
            //var xs = from w in stream.Window(TimeSpan.FromSeconds(1))
            //         from g in w.GroupBy(m => m.Level)
            //         from agg in Observable.Zip(
            //                                g.Count(),
            //                                g.LastAsync(),
            //                                (c, m) => (Count: c, Message: m))
            //         select agg;
            //xs.Subscribe(v => Save($"Count = {v.Count} ||  {v.Message}"));

            //var trigger = Observable.Timer(TimeSpan.FromSeconds(1));
            //var xs =  from g in stream.GroupByUntil(m => m.Level, m => m, g => trigger)
            //         from agg in Observable.Zip(
            //                                g.Count(),
            //                                g.LastAsync(),
            //                                (c, m) => (Count: c, Message: m))
            //         select agg;
            //xs.Subscribe(v => Save($"Count = {v.Count} \t||  {v.Message}"));

            //var trigger = Observable.Timer(TimeSpan.FromSeconds(1));
            //var xs = stream.GroupByUntil(m => m.Level, m => m, g => trigger)
            //                .SelectMany(g => Observable.Zip(
            //                                g.Count(),
            //                                g.LastAsync(),
            //                                (c, m) => (Count: c, Message: m)));
            //xs.Subscribe(v => Save($"Count = {v.Count} \t||  {v.Message}"));

            // #4 change #3, don't us fix time (1 second) use throttle 
            //var xs = stream.GroupByUntil(m => m.Level, m => m, g => g.Throttle(TimeSpan.FromSeconds(0.4)))
            //                .SelectMany(g => Observable.Zip(
            //                                g.Count(),
            //                                g.LastAsync(),
            //                                (c, m) => (Count: c, Message: m)));
            //xs.Subscribe(v => Save($"Count = {v.Count} \t||  {v.Message}"));
            // #5 change #4 use throttle or 10 seconds
            var xs = stream.GroupByUntil(m => m.Level, m => m, g => 
                                        Observable.Merge( g.Throttle(TimeSpan.FromSeconds(0.4)).Select(_ => 0L), 
                                                            Observable.Timer(TimeSpan.FromSeconds(4))))
                            .SelectMany(g => Observable.Zip(
                                            g.Count(),
                                            g.LastAsync(),
                                            (c, m) => (Count: c, Message: m)));
            xs.Subscribe(v => Save($"Count = {v.Count} \t||  {v.Message}"));
        }

        public void Save(string data)
        {
            Console.WriteLine(data);
            //Trace.WriteLine(data);
        }
    }
}
