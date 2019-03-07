using System;
using System.Collections.Generic;
using System.Text;

namespace _03_Logger
{
    public class LogComparer : EqualityComparer<LogMessage>
    {
        public static readonly EqualityComparer<LogMessage> LevelAndContent = new LogComparer();

        public override bool Equals(LogMessage x, LogMessage y) => x.Level == y.Level && x.Content == y.Content;

        public override int GetHashCode(LogMessage obj) => obj.Level.GetHashCode() ^ obj.Content.GetHashCode();
    }
}
