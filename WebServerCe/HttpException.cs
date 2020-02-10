using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WebServerCe
{
    public class HttpException: Exception
    {
        public HttpException(string message)
        :base(message)
        { }

        public HttpException(string message, Exception inner)
            : base(message, inner)
        { }

    }
}
