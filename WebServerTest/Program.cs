using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WebServerCe;
using System.Net;
using System.IO;

namespace WebServerTest
{
    class Program
    {
        public static string GetBaseDirectory()
        {
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            if (path.StartsWith("file:\\"))
                path = path.Substring(6);
            return path;
        }

        static void Main(string[] args)
        {
            string baseDir = GetBaseDirectory();
            FileQueryDispatcher files = new FileQueryDispatcher(Path.Combine(baseDir, @"..\..\www"), "/");
            DefaultQueryDispatcher root = new DefaultQueryDispatcher(Path.Combine(baseDir, @"..\..\www"));
            SimpleWebServer server = new SimpleWebServer(new ConsoleLogger());
            server.Dispatchers.Add(root);
            server.Dispatchers.Add(files);
            server.Dispatchers.Add(new DemoData());
            server.Dispatchers.Add(new PageNotFoundQueryDispatcher());

            server.Port = 8080;
            server.IPAddress = IPAddress.Any;
            server.Start();

            Console.ReadLine();
            server.Stop();
        }
    }
}
