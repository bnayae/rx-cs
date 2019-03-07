using System;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace _11_Buffer
{
    class Program
    {
        static void Main(string[] args)
        {
            var sub = new Subject<int>();
            var xs = sub.Do(m => Console.Write("*"))
                        .Buffer(3)
                        .Publish().RefCount();
            xs.Subscribe(m => Console.Write($" {m}"));


            sub.OnNext(1);
            xs.Subscribe(m => Console.Write($" {m}"));

            sub.OnNext(2);
            Console.ReadKey();

        }
    }
}
