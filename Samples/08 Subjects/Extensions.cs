using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace System.Reactive.Linq
{
    public static class Extensions
    {
        private static ISubject<long, long> _subject = new Subject<long>(); 

        public static IObservable<long> MyPublish(this IObservable<long> instance)
        {
            instance.Subscribe(_subject);
            return _subject;
        }
    }
}
