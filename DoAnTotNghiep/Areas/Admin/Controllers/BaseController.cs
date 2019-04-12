using DoAnTotNghiep.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoAnTotNghiep.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext executingContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                executingContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { Controller = "Login", Action = "Index", Areas = "Admin" }
                    ));
            }
            base.OnActionExecuting(executingContext);
        }
    }
}