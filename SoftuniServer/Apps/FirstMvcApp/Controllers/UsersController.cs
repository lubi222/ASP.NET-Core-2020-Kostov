using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SoftuniServer.HTTP;
using SoftuniServer.MvcFramework;

namespace FirstMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse Register(HttpRequest request)
        {
            return this.View();

        }

        public HttpResponse DoLogin(HttpRequest arg)
        {
            //TODO: read data; check user; log user; redirec user to home;
            return this.Redirect("/");
        }
    }
}
