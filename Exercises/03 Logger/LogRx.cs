using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace _03_Logger
{
    public class LogRx
    {
        public readonly static LogRx Default = new LogRx();

        private readonly Subject<LogMessage> _subject = new Subject<LogMessage>();
        public void Log(LogMessage message) => _subject.OnNext(message);

        public IObservable<LogMessage> Stream => _subject.AsObservable();
    }
}
