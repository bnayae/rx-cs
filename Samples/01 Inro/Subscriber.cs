using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Text;

namespace _01_Inro
{
    class Subscriber : IObserver<int>
    {
        public void OnNext(int value)
        {
            Console.Write($"{value}-");
        }

        public void OnCompleted()
        {
            Console.WriteLine("|");
        }

        public void OnError(Exception error)
        {
            Trace.WriteLine(error.ToString());
            Console.WriteLine("X");
        }
    }
}
