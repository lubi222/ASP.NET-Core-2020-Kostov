using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace SoftuniServer.HTTP
{
    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();

            var lines = requestString.Split(new string[] {HttpConstants.NewLine}, StringSplitOptions.None);

            var headerLine = lines[0];
            var headerLineParts = headerLine.Split(' ');
            this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), headerLineParts[0], true);
            this.Path = headerLineParts[1];

            int lineIndex = 1;
            bool isInHeaders = true;
            StringBuilder bodyBuilder = new StringBuilder();

            while (lineIndex < lines.Length)
            {
                var line = lines[lineIndex]; // potential Header
                lineIndex++;

                if (string.IsNullOrWhiteSpace(line)) // empty rows will signify that we are done with the headers and start working with the body 
                {
                    isInHeaders = false;
                }
                if (isInHeaders)
                {
                    this.Headers.Add(new Header(line)); // the header class contains logic to transform a line to a header 
                }
                else
                {
                    bodyBuilder.AppendLine(line);
                }


                // Checking whether the browser wants to send us cookies
                if (this.Headers.Any(h => h.Name == HttpConstants.RequestCookieHeader))
                {
                    var cookiesAsString = this.Headers.FirstOrDefault(h => h.Name == HttpConstants.RequestCookieHeader)
                        .Value;

                    var cookies = cookiesAsString.Split(new string[] {"; "}, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var cookieAsString in cookies)
                    {
                        this.Cookies.Add(new Cookie(cookieAsString));
                    }
                }

                this.Body = bodyBuilder.ToString();
            }
        }

        public string Path { get; set; }
        public HttpMethod Method { get; set; }
        public ICollection<Header> Headers { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
        public string Body { get; set; }
    }
}
