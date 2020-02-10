using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WebServerCe
{
    public class FileQueryDispatcher : IQueryDispatcher
    {
        private readonly string _baseDir;
        private readonly string _virtPath;

        public FileQueryDispatcher(string baseDir, string virtPath)
        {
            _baseDir = baseDir;
            _virtPath = virtPath;
        }

        public bool ProcessQuery(SimpleWebRequest request, HttpResponse response)
        {
            if (request.Path.Contains(".."))
            {
                return false;
            }

            if (!request.Path.StartsWith(_virtPath, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            string path = request.Path.Substring(_virtPath.Length).Replace('/', '\\');

            string fullPath = Path.Combine(_baseDir, path.Trim('\\'));
            if (File.Exists(fullPath))
            {
                using (var stream = File.OpenRead(fullPath))
                {
                    string mimeType = MimeTypeMap.GetMimeMapping(fullPath);
                    response.SendFile(HttpStatus.OK, stream, mimeType);
                    return true;
                }
            }
            else
            {
                if (Directory.Exists(fullPath))
                {
                    ListDirectory(request.Path, fullPath, request, response);
                    return true;
                }
            }
            return false;
        }

        private void ListDirectory(string path, string fullPath, SimpleWebRequest request, HttpResponse response)
        {
            string host;
            request.Options.TryGetValue("Host", out host);
            
            StringBuilder page = new StringBuilder();
            page.Append("<html>\n<head>\n");
            page.AppendFormat("<title>Content of: {0}</title>\n", path);
            page.Append("</head>\n<body>\n");
            page.AppendFormat("<h1>Content of: {0}</h1>\n", path);
            page.Append("<div>\n");
            foreach (string entry in Directory.GetFileSystemEntries(fullPath))
            {
                string ename = entry.Substring(fullPath.Length).Trim('\\', '/');
                string eUrl = string.Format("http://{0}{1}/{2}", host, 
                    path == "/" ? "" : Uri.EscapeUriString(path), 
                    Uri.EscapeUriString(ename));
                page.AppendFormat("<p><a href=\"{0}\">{1}</a></p>\n", eUrl, ename);
            }
            page.Append("</div></body>\n</html>\n");
            response.SendPage(HttpStatus.OK, page.ToString());
        }
    }
}
