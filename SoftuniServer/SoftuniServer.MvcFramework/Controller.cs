using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using SoftuniServer.HTTP;

namespace SoftuniServer.MvcFramework
{
    public abstract class Controller
    {
        public HttpResponse View([CallerMemberName]string viewPath = null)
        {
            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.html");

            var viewContent = System.IO.File.ReadAllText("Views/" + this.GetType().Name.Replace("Controller", string.Empty) + "/" + viewPath + ".html");

            var responseHtml = layout.Replace("@RenderBody()", viewContent); // allways render the layout, replace the renderBody() with the currentview

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }

        // a method we can use for returning a file from a controller 
        public HttpResponse File(string filePath, string contentType)
        {
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response = new HttpResponse(contentType, fileBytes);
            return response;
        }


        public HttpResponse Redirect(string url)
        {
            var response = new HttpResponse(HttpStatusCode.Found);
            response.Headers.Add(new Header("Location", url));
            return response;
        }
    }
}
