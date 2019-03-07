using System;
using System.Threading;

namespace _03_Logger
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var logger = LogRx.Default;
            var mail = new MailProvider(logger.Stream);
            var storage = new StorageProvider(logger.Stream);

            int i = 0;
            while (true)
            {
                i++;
                if (i % 7 == 0)
                {
                    var m = new LogMessage(LogLevel.Error, DateTime.UtcNow,
                        new IndexOutOfRangeException("Problem"));
                    logger.Log(m);
                }
                if (i % 13 == 0)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        var m = new LogMessage(LogLevel.Error, DateTime.UtcNow,
                                new FormatException("Some format error"));
                        logger.Log(m);

                    }
                }
                if (i % 3 == 0)
                {
                    var m = new LogMessage(LogLevel.Info, DateTime.UtcNow, "Some Info");
                    logger.Log(m);
                }
                var d = new LogMessage(LogLevel.Debug, DateTime.UtcNow, "Debugging indication");
                logger.Log(d);

                Thread.Sleep(100);
            }
        }
    }
}
