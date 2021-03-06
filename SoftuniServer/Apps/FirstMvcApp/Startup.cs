﻿using System;
using System.Collections.Generic;
using System.Text;
using FirstMvcApp.Controllers;
using SoftuniServer.HTTP;
using SoftuniServer.MvcFramework;

namespace FirstMvcApp
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices()
        {
            throw new NotImplementedException();
        }

        public void Configure(List<Route> routeTable)
        {
            routeTable.Add(new Route("/", HttpMethod.Get, new HomeController().Index));
            routeTable.Add(new Route("/users/login", HttpMethod.Get, new UsersController().Login));
            routeTable.Add(new Route("/users/login", HttpMethod.Post, new UsersController().DoLogin));
            routeTable.Add(new Route("/users/register", HttpMethod.Get, new UsersController().Register));
            routeTable.Add(new Route("/cards/add", HttpMethod.Get, new CardsController().Add));
            routeTable.Add(new Route("/cards/all", HttpMethod.Get, new CardsController().All));
            routeTable.Add(new Route("/cards/collection", HttpMethod.Get, new CardsController().Collection));
        }

    }
}
