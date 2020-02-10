using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WebServerCe
{
    public interface IQueryDispatcher
    {
        bool ProcessQuery(SimpleWebRequest request, HttpResponse response);
    }
}
