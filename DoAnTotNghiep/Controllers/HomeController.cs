using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.Dao;
using System.Web.Mvc;
using Model.EF;
using System.Data.Entity;
using Newtonsoft.Json;
using DoAnTotNghiep.ViewModel;

namespace DoAnTotNghiep.Controllers
{
    public class HomeController : Controller
    {
        MilkTeaDbContext db = new MilkTeaDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult LoadAllProduct()
        //{
        //    var result = new ProductDao().GetListAllProduct().Take(8);
        //    return Content(JsonConvert.SerializeObject(new
        //    {
        //        result
        //    }));
        //}
        public ActionResult LoadAllProduct()
        {
            var query = db.Products
             .Include(s => s.Supplier)
             .Select(s => new
             {
                 ProductID = s.ProductID,
                 Price = s.Price,
                 PriceImport = s.PriceImport,
                 Image = s.Image,
                 IsHot = s.IsHot,
                 IsNew = s.IsNew,
                 ProductName = s.ProductName,
                 MetaTitle = s.MetaTitle,
                 SupperlierName = s.Supplier.SupperlierName
             }).Take(8).OrderBy(s => s.ProductID).ToList();
            return Content(JsonConvert.SerializeObject(new
            {
                query
            }));
        }

        public ActionResult ProductDetail(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public ActionResult GetProductModal(int Id)
        {
            //var query = db.Products.Find(Id);
            var query = from product in db.Products
                        join productCategory in db.ProductCategories
                        on product.CategoryID equals productCategory.ID

                        join supplier in db.Suppliers
                        on product.SupplierID equals supplier.SupperlierID

                        // infonutri in db.Nutritionalinformations
                        //on product.
                        where product.ProductID == Id
                        select new
                        {
                            ProductID = product.ProductID,
                            ProductName = product.ProductName,
                            Price = product.Price,
                            MetaTitle = product.MetaTitle,
                            SupplierName = supplier.SupperlierName,
                            Image = product.Image,
                            CategoryName = productCategory.Name,
                            Description = product.Description,

                        };
            var result = query.ToList();
            var listSize = db.Sizes.ToList();
            var listStone = db.AmountOfStones.ToList();
            var listSugar = db.AmountOfSugars.ToList();
            var listTopping = db.Toppings.ToList();
            return Content(JsonConvert.SerializeObject(new
            {
                result,
                listSize,
                listStone,
                listSugar,
                listTopping
            }));
        }

        public ActionResult RelatedProduct(int Id)
        {
            Product sp = db.Products.Find(Id);
            var query = from product in db.Products
                        join productCategory in db.ProductCategories
                        on product.CategoryID equals productCategory.ID

                        join supplier in db.Suppliers
                        on product.SupplierID equals supplier.SupperlierID

                        where product.ProductID != Id && product.CategoryID == sp.CategoryID
                        select new
                        {
                            ProductID = product.ProductID,
                            ProductName = product.ProductName,
                            Price = product.Price,
                            MetaTitle = product.MetaTitle,
                            SupplierName = supplier.SupperlierName,
                            Image = product.Image,
                            CategoryName = productCategory.Name,
                            Description = product.Description,

                        };
            var result = query.ToList();
            ViewBag.RelatedProduct = result;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}