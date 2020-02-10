using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WebServerCe
{
    public enum HttpMethod
    {
        None,
        GET,
        HEAD,
        POST,
        PUT,
        PATCH,
        DELETE,
        TRACE,
        CONNECT
    }
}
