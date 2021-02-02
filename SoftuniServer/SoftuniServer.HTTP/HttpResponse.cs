using System;
using System.Collections.Generic;
using System.Text;

namespace SoftuniServer.HTTP
{
    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }
        public HttpResponse(string contentType, byte[] body, HttpStatusCode statusCode = HttpStatusCode.Ok)
        {
            if(body == null) 
            {
                throw new ArgumentException(nameof(body));
            }

            this.StatusCode = StatusCode;
            this.Body = body;
            this.Headers = new List<Header>()
            {
                {new Header("Content-Type", contentType)},
                {new Header("Content-Length", body.Length.ToString())},
            };
            this.Cookies = new List<Cookie>();
        }

        public override string ToString()
        {
            StringBuilder responseBuilder = new StringBuilder();

            responseBuilder.Append($"HTTP/1.1 {(int) this.StatusCode} {this.StatusCode}" + HttpConstants.NewLine);
            foreach (var header in this.Headers)
            {
                responseBuilder.Append(header.ToString() + HttpConstants.NewLine);
            }

            foreach (var cookie in this.Cookies)
            {
                responseBuilder.Append("Set-Cookie: " + cookie.ToString() + HttpConstants.NewLine);
            }

            responseBuilder.Append(HttpConstants.NewLine);
            return responseBuilder.ToString();
        }

        public ICollection<Header> Headers { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public byte[] Body { get; set; }
    }
}


