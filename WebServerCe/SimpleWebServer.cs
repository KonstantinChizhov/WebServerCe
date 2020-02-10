using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace WebServerCe
{
    public class SimpleWebServer
    {

        private TcpListener _tcpListener;
        private Thread _thread;
        private volatile bool _running = false;
        private readonly ILog _log;

        public int Port { get; set; }
        public IPAddress IPAddress { get; set; }
        public string Name { get; set; }
        public List<IQueryDispatcher> Dispatchers { get; private set; }

        public SimpleWebServer(ILog log)
        {
            _log = log;
            Port = 80;
            IPAddress = IPAddress.Any;
            Name = "Web Server Ce";
            Dispatchers = new List<IQueryDispatcher>();
        }

        public void Start()
        {
            try
            {
                _log.Info("Starting WEB server for IP address: {0}, port: {1}", IPAddress, Port);
                _tcpListener = new TcpListener(IPAddress, Port);
                _tcpListener.Start();
                _thread = new Thread(ListenerTread);
                _running = true;
                _thread.Start();
                _log.Info("WEB server started");
            }
            catch (Exception e)
            {
                _log.Error("Error starting WEB server: {0}", e);
            }
        }

        private void ListenerTread()
        {
            while (_running)
            {
                try
                {
                    ListnerLoop();
                }
                catch (Exception e)
                {
                    _log.Error("Error in WEB server listner loop: {0}", e);
                }
            }
        }

        void ListnerLoop()
        {
            try
            {
                using (TcpClient tcpClient = _tcpListener.AcceptTcpClient())
                {
                    HttpResponse response = new HttpResponse(tcpClient);
                    try
                    {
                        _log.Debug("Client conected: {0}", tcpClient.Client.RemoteEndPoint);
                        if (!tcpClient.Client.Connected)
                        {
                            return;
                        }
                        string requestSb = GetRequestString(tcpClient);
                        if (string.IsNullOrEmpty(requestSb))
                        {
                            return;
                        }
                        SimpleWebRequest request = new SimpleWebRequest(requestSb);
                        _log.Debug("Request: {0}", requestSb);
                        ProcessRequest(request, response);
                    }
                    catch (HttpException he)
                    {
                        _log.Error("Parcing HTTP request: {0}", he);
                        response.SendPage(HttpStatus.BadRequest, he.Message);
                    }
                }
            }
            catch (SocketException se)
            {
                _log.Error("Error accepting client: {0}", se);
            }
        }

        private void ProcessRequest(SimpleWebRequest request, HttpResponse response)
        {
            bool handled = false;
            foreach (var dispatcher in Dispatchers)
            {
                handled = dispatcher.ProcessQuery(request, response);
                if (handled)
                {
                    break;
                }
            }
            if (!handled)
            {
                response.SendPage(HttpStatus.NotFound, "Requested page is not found");
            }
        }

        private static string GetRequestString(TcpClient tcpClient)
        {
            int bytesRead;
            StringBuilder requestSb = new StringBuilder(4096);
            byte[] buffer = new byte[4096];

            NetworkStream stream = tcpClient.GetStream();
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string str = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
                requestSb.Append(str);
                if (str.IndexOf("\r\n\r\n") >= 0)
                {
                    break;
                }
            }
            return requestSb.ToString();
        }

        public void Stop()
        {
            try
            {
                if (_thread != null)
                {
                    _log.Info("Stopping WEB server");
                    _running = false;
                    _tcpListener.Stop();
                    _thread.Join(1000);
                    _thread = null;
                    _tcpListener = null;
                    _log.Info("WEB server stopped");
                }
            }
            catch (Exception e)
            {
                _log.Error("Error stopping WEB server: {0}", e);
            }
        }


    }
}
