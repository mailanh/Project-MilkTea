using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Model.Dao;
using Model.EF;
using DoAnTotNghiep.Models;
using System.Data.Entity;
namespace DoAnTotNghiep.Controllers
{
    public class PaymentController : Controller
    {
        private readonly MilkTeaDbContext db = new MilkTeaDbContext();
        private readonly Messages messages = new Messages();
        //private readonly CartController cartController;
        //private readonly CustomerDao customerDao;
        private List<CartItem> cartItems = null;
        //public PaymentController(CartController _cartController, CustomerDao _customerDao, MilkTeaDbContext _db)
        //{
        //    cartController = _cartController;
        //    customerDao = _customerDao;
        //    db = _db;
        //}
        //public PaymentController()
        //{
        //}
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["CartItems"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Notfound404", "Payment");
            }
        }

        public ActionResult Notfound404()
        {
            return View();
        }
        public List<CartItem> GetListCart()
        {
            cartItems = Session["CartItems"] as List<CartItem>;
            if (cartItems == null)
            {
                cartItems = new List<CartItem>();
                Session["CartItems"] = cartItems;
            }
            return cartItems;
        }

        public ActionResult AddOrder(Customer customer, Order order)
        {
            var listCart = Session["CartItems"] as List<CartItem>;
            List<CartItem> carts = GetListCart();

            if (customer != null)
            {
                if (customer.CustomerID != 0)
                {
                    #region Khách đăng nhập bằng tài khoản
                    Customer cm = db.Customers.Find(customer.CustomerID);
                    order.CustomerID = customer.CustomerID;
                    //order.OrderDate = DateTime.Now;
                    order.Total = listCart.Sum(s => s.Totals);
                    order.Status = 0;
                    db.Orders.Add(order);
                    db.SaveChanges();
                    int orderID = order.OrderID;
                    foreach (var item in carts)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderID = orderID;
                        orderDetail.ProductID = item.ProductID;
                        orderDetail.ToppingID = JsonConvert.SerializeObject(item.ToppingID1);
                        orderDetail.SizeID = item.SizeID;
                        orderDetail.AmountOfStone = item.AmountOfStone;
                        orderDetail.AmountOfSugar = item.AmountOfSugar;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.Status = true;
                        db.OrderDetails.Add(orderDetail);
                        db.SaveChanges();
                    }
                    #endregion
                }
                else
                {

                    #region Khách hàng vãng lai
                    // Add new customer
                    //customerDao.InsertCustomer(customer);
                    db.Customers.Add(customer);
                    db.SaveChanges();

                    // Add new order
                    int CustomerID = customer.CustomerID;
                    order.CustomerID = CustomerID;
                    order.OrderDate = DateTime.UtcNow;
                    order.Total = listCart.Sum(s => s.Totals);
                    order.Status = 0;
                    db.Orders.Add(order);
                    db.SaveChanges();

                    // Add order details
                    int orderID = order.OrderID;
                    foreach (var item in carts)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderID = orderID;
                        orderDetail.ProductID = item.ProductID;
                        orderDetail.ToppingID = JsonConvert.SerializeObject(item.ToppingID1);
                        orderDetail.SizeID = item.SizeID;
                        orderDetail.AmountOfStone = item.AmountOfStone;
                        orderDetail.AmountOfSugar = item.AmountOfSugar;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.Status = true;
                        db.OrderDetails.Add(orderDetail);
                        db.SaveChanges();
                    }
                    #endregion
                    Session["CartItems"] = null;
                    Session["OrderID"] = orderID;
                    messages.Success = true;
                    messages.Contents = orderID.ToString();
                    return Content(JsonConvert.SerializeObject(new
                    {
                        result = messages,
                    }));
                }
            }
            return View();
        }

        public ActionResult ViewsOrder()
        {
            if (Session["OrderID"] != null)
            {
                ViewBag.OrderID = Session["OrderID"];
                Session["OrderID"] = null;
                return View();
            }
            else
            {
                return RedirectToAction("Notfound404", "Payment");
            }
        }

        public ActionResult ViewOrderDetails(int ID)
        {
            //var result = db.Orders.Include(s => s.Customer).Where(s => s.OrderID == ID).ToList();
            var query = from order in db.Orders
                        .Where(s => s.OrderID == ID)
                        join customer in db.Customers
                        on order.CustomerID equals customer.CustomerID
                        select new
                        {
                            OrderID = order.OrderID,
                            OrderDate = order.OrderDate,
                            Status = order.Status,
                            customerID = customer.CustomerID,
                            Total = order.Total,
                            CustomerName = customer.Name,
                            Phone = customer.Phone,
                            Gender = customer.Gender,
                            DateOfBirth = customer.DateOfBirth,
                            Address = customer.Address,
                            Gmail = customer.Gmail
                        };
            var result = query.ToList();
            return Content(JsonConvert.SerializeObject(new
            {
                result,
            }));
        }
    }
}