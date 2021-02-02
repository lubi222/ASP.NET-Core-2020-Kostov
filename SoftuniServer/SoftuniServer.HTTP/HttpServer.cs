using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SoftuniServer.MvcFramework;

namespace SoftuniServer.HTTP
{
    public class HttpServer : IHttpServer
    {

        private List<Route> routeTable = new List<Route>();

        public HttpServer(List<Route> routeTable)
        {
            this.routeTable = routeTable;
        }

        public async Task StartAsync(int port)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync(); // we need the result from this method in order to go forward so we use await
                ProcessClientAsync(tcpClient); // but we do not need this result so we do not await here 
            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            try
            {

            
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    List<byte> data = new List<byte>();
                    byte[] buffer = new byte[HttpConstants.BufferSize];
                    int position = 0;
                    
                    while (true)
                    {
                        int count = await stream.ReadAsync(buffer, position, buffer.Length);

                        position += count;

                        if (count < buffer.Length)
                        {
                            var partialBuffer = new byte[count];
                            Array.Copy(buffer, partialBuffer, count);
                            data.AddRange(partialBuffer);
                            break;
                        }
                        
                        data.AddRange(buffer);

                        if (count == 0)
                        {
                            break;
                        }

                    }

                    // byte[] => string (text)

                    var requestAsString = Encoding.UTF8.GetString(data.ToArray());
                    var request = new HttpRequest(requestAsString);
                    Console.WriteLine(request.Method);

                    HttpResponse response;
                    var route = this.routeTable.FirstOrDefault(r => string.Compare(r.Path, request.Path, true) == 0 && r.Method == request.Method);
                    if (route != null)
                    {
                        response = route.Action(request);
                    }
                    else
                    {
                        response = new HttpResponse("text/html", new byte[0], HttpStatusCode.NotFound);
                    }

                    var responseHeaderBytes = Encoding.UTF8.GetBytes(response.ToString());

                    await stream.WriteAsync(responseHeaderBytes, 0, responseHeaderBytes.Length);
                    await stream.WriteAsync(response.Body, 0, response.Body.Length);

                    Console.WriteLine(requestAsString);
                }
                tcpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
