using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Contrib.Monitoring;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Visual_Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var info = await VisualRxSettings.Initialize(VisualRxWcfDiscoveryProxy.Create());
            Console.WriteLine(info);
            var xs = Observable.Interval(TimeSpan.FromSeconds(1))
                                .Monitor("Before", 1)
                                .Select(v => new string('*', (int)(v + 1)))
                                .Monitor("After", 2)
                                .Subscribe(v => Console.Write($"{v},"));

            Console.ReadKey();
        }
    }
}
