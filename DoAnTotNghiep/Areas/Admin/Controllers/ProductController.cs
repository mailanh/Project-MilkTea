using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using Newtonsoft.Json;

namespace DoAnTotNghiep.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly MilkTeaDbContext db = new MilkTeaDbContext();
        private readonly ProductDao productDao = new ProductDao();
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllProduct()
        {
            var result = productDao.GetAllProduct();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}