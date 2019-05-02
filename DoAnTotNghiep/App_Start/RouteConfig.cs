using System;
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
            //routes.IgnoreRoute("");
            routes.MapRoute(
                 "Product Detail",
                 "{chi-tiet}/{metatitle}-{id}",
                 new { controller = "Home", action = "ProductDetail", id = UrlParameter.Optional },
                 new[] { "DoAnTotNghiep.Controllers" }
            );

            routes.MapRoute(
                 "Shopping Cart",
                 "gio-hang",
                 new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                 new[] { "DoAnTotNghiep.Controllers" }
            );

            routes.MapRoute(
                 "Check out",
                 "thanh-toan",
                 new { controller = "Payment", action = "Index", id = UrlParameter.Optional },
                 new[] { "DoAnTotNghiep.Controllers" }
            );

            routes.MapRoute(
                 "Error",
                 "loi-404",
                 new { controller = "Payment", action = "Notfound404", id = UrlParameter.Optional },
                 new[] { "DoAnTotNghiep.Controllers" }
            );

            routes.MapRoute(
                "Check Order",
                "xac-nhan-don-hang-cua-ban",
                new { controller = "Payment", action = "ViewsOrder", id = UrlParameter.Optional },
                new[] { "DoAnTotNghiep.Controllers" }
           );

            routes.MapRoute(
                 "Default",
                 //"{controller}/{action}/{id}",
                 "{controller}/{action}/{id}",
                 new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 new[] { "DoAnTotNghiep.Controllers" }
            );
        }
    }
}
