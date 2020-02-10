using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WebServerCe
{
    public interface ILog
    {
        void Info(string format, params object[] args);
        void Error(string format, params object[] args);
        void Warning(string format, params object[] args);
        void Error(string message, Exception ex);
        void Debug(string format, params object[] args);
    }
}
