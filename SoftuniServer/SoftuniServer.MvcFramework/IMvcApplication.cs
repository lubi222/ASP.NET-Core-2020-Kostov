using System;
using System.Collections.Generic;
using System.Text;

namespace SoftuniServer.MvcFramework
{
    public interface IMvcApplication
    {
        void ConfigureServices();
        void Configure(List<Route> routeTable);
    }
}
