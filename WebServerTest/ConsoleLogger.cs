using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WebServerCe;

namespace WebServerTest
{
    public class ConsoleLogger: ILog
    {

        #region ILog Members

        public void Info(string format, params object[] args)
        {
            Console.WriteLine("Info: " + format, args);
        }

        public void Error(string format, params object[] args)
        {
            Console.WriteLine("Error: " + format, args);
        }

        public void Warning(string format, params object[] args)
        {
            Console.WriteLine("Warning: " + format, args);
        }

        public void Error(string message, Exception ex)
        {
            Console.WriteLine("Error: " + message, ex);
        }

        public void Debug(string format, params object[] args)
        {
            Console.WriteLine("Debug: " + format, args);
        }

        #endregion
    }
}
