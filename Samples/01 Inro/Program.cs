using System;
using System.Threading;

namespace _01_Inro
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Producer();
            var c = new Subscriber();

            IDisposable subscription = p.Subscribe(c);

            Console.WriteLine("Continue");
            Thread.Sleep(3200);
            subscription.Dispose();
            Console.ReadKey();
        }
    }
}
