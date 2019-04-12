﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoAnTotNghiep
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 "Product Detail",
                 "{chi-tiet}/{metatitle}-{id}",
                 new { controller = "Home", action = "ProductDetail", id = UrlParameter.Optional },
                 new[] { "DoAnTotNghiep.Controllers" }
            );

            routes.MapRoute(
                 "Shopping Cart",
                 "{gio-hang}/{metatitle}-{id}",
                 new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                 new[] { "DoAnTotNghiep.Controllers" }
            );

            routes.MapRoute(
                 "Default",
                 "{controller}/{action}/{id}",
                 new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 new[] { "DoAnTotNghiep.Controllers" }
            );
        }
    }
}
