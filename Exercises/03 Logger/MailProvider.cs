using System;
using System.Collections.Generic;
using System.Text;

namespace _03_Logger
{
    public class MailProvider
    {
        public MailProvider(IObservable<LogMessage> stream)
        {
            // TODO: 
        }

        public void Send(string data)
        {
            Console.WriteLine($@"
 _________
|\       /|
| \     / |
|  `...'  |
|__/___\__|

{data}
");
        }
    }
}
