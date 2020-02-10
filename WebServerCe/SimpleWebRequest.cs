using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WebServerCe
{
    public class SimpleWebRequest
    {
        public HttpMethod Method { get; private set; }
        public IDictionary<string, string> Options { get; private set; }
        public string Path { get; private set; }
        public string ParamsStr { get; private set; }
        public IDictionary<string, string> Params { get; private set; }

        public SimpleWebRequest(string message)
            : this(message, false)
        {
        }

        public SimpleWebRequest(string message, bool skipOptions)
        {
            Params = new Dictionary<string, string>();
            string[] lines = message.Split('\r', '\n');
            if (lines.Length < 1)
            {
                throw new HttpException("Empty Http query");
            }
            string[] queryParts = lines[0].Split(' ', '\t');
            if (queryParts.Length < 2)
            {
                throw new HttpException("Invalid Http query");
            }
            Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), queryParts[0], true);
            int indexQ = queryParts[1].IndexOf('?');
            if (indexQ < 0)
            {
                Path = Uri.UnescapeDataString(queryParts[1]);
            }
            else
            {
                Path = Uri.UnescapeDataString(queryParts[1].Substring(0, indexQ));
                ParamsStr = queryParts[1].Substring(indexQ + 1).Trim();
                string[] keyValues = ParamsStr.Split(';', '&');
                foreach (string keyValue in keyValues)
                {
                    if (string.IsNullOrEmpty(keyValue))
                    {
                        continue;
                    }
                    int delimIndex = keyValue.IndexOf('=');
                    if (delimIndex < 0)
                    {
                        continue;
                    }
                    string name = Uri.UnescapeDataString(keyValue.Substring(0, delimIndex).Trim());
                    string value = Uri.UnescapeDataString(keyValue.Substring(delimIndex + 1).Trim());
                    Params[name] = value;
                }
            }
            if (skipOptions)
            {
                return;
            }
            Options = new Dictionary<string, string>();
            foreach (string line in lines.Skip(1))
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                int delimIndex = line.IndexOf(':');
                if (delimIndex < 0)
                {
                    continue;
                }
                string name = line.Substring(0, delimIndex).Trim();
                string value = line.Substring(delimIndex + 1).Trim();
                Options[name] = value;
            }
        }
    }
}
