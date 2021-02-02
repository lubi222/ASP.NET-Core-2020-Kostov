using System;
using System.Collections.Generic;
using System.Text;
using SoftuniServer.HTTP;
using SoftuniServer.MvcFramework;

namespace FirstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            return this.View();
        }

    }
}
