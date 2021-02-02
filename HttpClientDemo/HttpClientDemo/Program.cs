using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpClientDemo
{
    class Program
    {
        private static Dictionary<string, int> SessionStorage = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            const string NewLine = "\r\n";
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);

            tcpListener.Start();

            // daemon // service
            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                using (var stream = client.GetStream())
                {


                    byte[] buffer = new byte[1000000];
                    var length = stream.Read(buffer, 0, buffer.Length);

                    string requestString = Encoding.UTF8.GetString(buffer, 0, length);

                    Console.WriteLine(requestString);
                    var sid = Guid.NewGuid().ToString();

                    var match = Regex.Match(requestString, @"sid=[^\n]*\r\n");
                    if (match.Success)
                    {
                        sid = match.Value.Substring(4);
                    }

                    if (!SessionStorage.ContainsKey(sid))
                    {
                        SessionStorage.Add(sid, 0);
                    }

                    int timesOpened = SessionStorage[sid]++;

                    Console.WriteLine(sid);
                    string html = $"<h1>Hello from NikiServer {DateTime.Now} for the {timesOpened}</h1>" + 
                                  $"<form method=post><input name=username /><input name=password />" + 
                                  $"<input type=submit /></form>";

                    string response = "HTTP/1.1 200 OK" + NewLine +
                                      "Server: NikiServer 2020" + NewLine +
                                      "Content-Type: text/html; charset=utf-8" + NewLine +
                                      "X-Server-Version: 1.0" + NewLine + 
                                      //"Set-Cookie: sid=394288739439943250869; Domain=localhost; Path=/account;" + NewLine +
                                      $"Set-Cookie: sid={sid}; Expires=" + DateTime.UtcNow.AddHours(1).ToString("R") + NewLine + 
                                      "Content-Length: " + html.Length + NewLine +
                                      NewLine +
                                      html + NewLine;

                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseBytes);



                    Console.WriteLine(new string('=', 60));
                }
            }
        }

        public static async Task ReadData()
        {
            string url = "https://softuni.bg/courses/csharp-web-basics";
            HttpClient httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            Console.WriteLine(html);
        }
    }
}
