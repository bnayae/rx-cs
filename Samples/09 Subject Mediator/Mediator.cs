using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace _09_Subject_Mediator
{
    class Mediator<T> 
    {
        private readonly ISubject<T> _subject = new ReplaySubject<T>(TimeSpan.FromSeconds(1));
        public IObservable<T> Source => _subject;
        public IObserver<T> Target => _subject;
    }
}
