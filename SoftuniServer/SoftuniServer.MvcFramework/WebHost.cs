using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoftuniServer.HTTP;

namespace SoftuniServer.MvcFramework
{
    public static class WebHost
    {
        public static async Task RunAsync(IMvcApplication application, int port)
        {
            List<Route> routeTable = new List<Route>();
            application.ConfigureServices();
            application.Configure(routeTable);

            IHttpServer server = new HttpServer(routeTable);



            await server.StartAsync(port);
        }
    }
}
