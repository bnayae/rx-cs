using System;
using System.Collections.Generic;
using System.Text;

namespace _03_Logger
{
    public class LogMessage
    {
        public LogMessage(LogLevel level, DateTime date, string content)
        {
            Level = level;
            Date = date;
            Content = content;
        }

        public LogMessage(LogLevel level, DateTime date, Exception exception)
        {
            Level = level;
            Date = date;
            Content = exception.Message;
            Exception = exception;
        }

        public LogLevel Level { get; }
        public DateTime Date { get; }
        public string Content { get; }
        public Exception Exception { get; }

        public override string ToString() => $"{Date:yyy-MM-dd HH:mm:ss} [{Level}]: {Content}, Error = {Exception}";

    }


}
