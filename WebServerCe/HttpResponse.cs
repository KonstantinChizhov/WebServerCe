using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.IO.Compression;

namespace WebServerCe
{
    public class HttpResponse
    {
        private readonly TcpClient _tcpClient;
        public IPEndPoint LocalEndpoint { get { return (IPEndPoint)_tcpClient.Client.LocalEndPoint; } }

        public HttpResponse(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }

        static Dictionary<HttpStatus, string> HttpStatusToString = new Dictionary<HttpStatus, string>
        {
            {HttpStatus.BadRequest, "Bad Request"},
            {HttpStatus.NotFound, "NotFound"},
            {HttpStatus.InternalServerError, "Internal Server Error"},
        };

        public string GetHttpStatusString(HttpStatus httpStatus)
        {
            string result;
            if (!HttpStatusToString.TryGetValue(httpStatus, out result))
            {
                result = httpStatus.ToString();
            }
            return string.Format("{0} {1}", (int)httpStatus, result);
        }

        private void SendHeader(HttpStatus httpStatus, long totalBytes, string mimeType)
        {
            SendHeader(httpStatus, totalBytes, mimeType, false);
        }

        private void SendHeader(HttpStatus httpStatus, long totalBytes, string mimeType, bool compressed)
        {
            if (string.IsNullOrEmpty(mimeType))
            {
                mimeType = "text/html";
            }

            string statusStrng = GetHttpStatusString(httpStatus);
            StringBuilder header = new StringBuilder();
            header.AppendFormat("HTTP/1.1 {0}\r\n", statusStrng);
            header.AppendFormat("Content-Type: {0}; charset=utf-8\r\n", mimeType);
            header.AppendFormat("Accept-Ranges: bytes\r\n");
            header.AppendFormat("Server: {0}\r\n", "Web server CE");
            header.AppendFormat("Connection: close\r\n");
            if (!compressed)
            {
                header.AppendFormat("Content-Length: {0}\r\n", totalBytes);
            }
            if (compressed)
            {
                header.Append("Content-Encoding: deflate\r\n");
            }
            header.Append("\r\n");

            byte[] bytes = Encoding.UTF8.GetBytes(header.ToString());
            if (!_tcpClient.Client.Connected)
            {
                return;
            }
            _tcpClient.GetStream().Write(bytes, 0, bytes.Length);
        }

        public void SendFile(HttpStatus httpStatus, Stream stream, string mimeType)
        {
            bool compress = MimeTypeMap.ShouldCompress(mimeType);
            SendHeader(httpStatus, stream.Length, mimeType, compress);
            Send(stream, compress);
        }

        public void SendPage(HttpStatus httpStatus, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            if (!_tcpClient.Client.Connected)
            {
                return;
            }
            SendHeader(httpStatus, bytes.Length, null);
            if (!_tcpClient.Client.Connected)
            {
                return;
            }
            _tcpClient.GetStream().Write(bytes, 0, bytes.Length);
        }

        private void Send(Stream stream, bool compress)
        {
            byte[] bytes = new byte[4096 * 4];
            int bytesRead = 0;
            if (!_tcpClient.Client.Connected)
            {
                return;
            }
            using (NetworkStream netStream = _tcpClient.GetStream())
            {
                if (compress)
                {
                    using (Stream compressedStream = new DeflateStream(netStream, CompressionMode.Compress))
                    {
                        while ((bytesRead = stream.Read(bytes, 0, bytes.Length)) > 0)
                        {
                            compressedStream.Write(bytes, 0, bytesRead);
                        }
                    }
                }
                else
                {
                    while ((bytesRead = stream.Read(bytes, 0, bytes.Length)) > 0)
                    {
                        netStream.Write(bytes, 0, bytesRead);
                    }
                }
            }
        }
    }
}
