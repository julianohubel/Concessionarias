﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Concessionaria
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           // routes.MapRoute(
           //    name: "CarroDetalhes",
           //    url: "Carros/Detalhes/{nome}",
           //    defaults: new { controller = "Carroes", action = "DetailsName" }
           //);


            // routes.MapRoute(
            //    name: "FabricateDetalhes",
            //    url: "Fabricantes/{nome}",
            //    defaults: new { controller = "Fabricantes", action = "DetailsName" }
            //);

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );



        }
    }
}
