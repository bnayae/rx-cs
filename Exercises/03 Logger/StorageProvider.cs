using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Text;

namespace _03_Logger
{
    public class StorageProvider
    {
        public StorageProvider(IObservable<LogMessage> stream)
        {

            // TODO: 
            // #1 Split Debug level (handle it on below normal thread priority)
            //    default handling for all other log level

            // #2 ignore log when repeat 

            // #3 when log (of specific type) repeat multiple time in 1 second
            //    write the count and content of the last repetition

            // #4 change #3, don't us fix time (1 second) use throttle 
            // #5 change #4 use throttle or 10 seconds
        }

        public void Save(string data)
        {
            Trace.WriteLine(data);
        }
    }
}
