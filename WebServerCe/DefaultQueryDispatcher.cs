using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WebServerCe
{
    public class DefaultQueryDispatcher : IQueryDispatcher
    {
        private readonly string _baseDir;
        private readonly string _virtPath;

        public DefaultQueryDispatcher(string baseDir)
        {
            _baseDir = baseDir;
        }

        public bool ProcessQuery(SimpleWebRequest request, HttpResponse response)
        {
            if (!(string.IsNullOrEmpty(request.Path) || request.Path == "/"))
            {
                return false;
            }
            string path = "index.html";
            string fullPath = Path.Combine(_baseDir, path);
            if (File.Exists(fullPath))
            {
                using (var stream = File.OpenRead(fullPath))
                {
                    string mimeType = MimeTypeMap.GetMimeMapping(fullPath);
                    response.SendFile(HttpStatus.OK, stream, mimeType);
                    return true;
                }
            }
            return false;
        }
    }
}
