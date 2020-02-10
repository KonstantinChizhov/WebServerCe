using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace WebServerCe
{
    public class PageNotFoundQueryDispatcher : IQueryDispatcher
    {
        public PageNotFoundQueryDispatcher()
        {

        }

        public bool ProcessQuery(SimpleWebRequest request, HttpResponse response)
        {
            response.SendPage(HttpStatus.NotFound, GetErrorPage(request, response, HttpStatus.NotFound, "Requested page is not found"));
            return true;
        }

       
        private string GetErrorPage(SimpleWebRequest request, HttpResponse response, HttpStatus httpStatus, string message)
        {
            string statusStrng = response.GetHttpStatusString(httpStatus);
            IPEndPoint ep = response.LocalEndpoint;

            StringBuilder errorPage = new StringBuilder();
            errorPage.Append("<html>\n<head>\n");
            errorPage.AppendFormat("<title>{0}</title>\n", statusStrng);
            errorPage.Append("</head>\n<body>\n");
            errorPage.AppendFormat("<h1>{0}</h1>\n", statusStrng);
            errorPage.AppendFormat("<p>{0}</p>\n", message);
            errorPage.Append("<hr>\n");
            errorPage.AppendFormat("</address>\"{0}\" Server at {1} Port {2} </address>\n", "Web server CE", ep.Address, ep.Port);

            errorPage.Append("</body>\n</html>\n");
            return errorPage.ToString();
        }
    }
}
