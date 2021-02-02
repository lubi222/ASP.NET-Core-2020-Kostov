using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using FirstMvcApp.Controllers;
using SoftuniServer.HTTP;
using SoftuniServer.MvcFramework;

namespace FirstMvcApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<Route> routeTable = new List<Route>();

            IHttpServer server = new HttpServer(routeTable);

           

            // using await because we care about the result
            await WebHost.RunAsync(new Startup(), 80); // if we do not use await the program just closes because we do not care about the result from the method
        }
        
        
    }

}
