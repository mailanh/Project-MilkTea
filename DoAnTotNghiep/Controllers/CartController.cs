using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoAnTotNghiep.Models;
using DoAnTotNghiep.ViewModel;
using Model.EF;
using Newtonsoft.Json;

namespace DoAnTotNghiep.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private MilkTeaDbContext db = new MilkTeaDbContext();
        private Messages messages = new Messages();
        private List<CartItem> cartItems = null;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllCart()
        {
            List<CartItem> ListCartItem = Session["CartItems"] as List<CartItem>;
            return Content(JsonConvert.SerializeObject(new
            {
                ListCartItem
            }));
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

        public IEnumerable<CartItemViewModel> GetCartItemViewModel(int productID)
        {
            var query = from product in db.Products
                                          .Where(s => s.ProductID == productID)
                        join size in db.Sizes on product.SizeID equals size.SizeID
                        join topping in db.Toppings on product.ToppingID equals topping.ID
                        select new CartItemViewModel
                        {
                            ProductID = product.ProductID,
                            ProductName = product.ProductName,
                            Images = product.Image,
                            Price = product.Price,
                            Quantity = product.Quantity,
                            AmountOfStone = product.AmountOfStoneID,
                            AmountOfSugar = product.AmountOfSugarID,
                            SizeID = size.SizeID,
                            UnitPrice = size.UnitPrice,
                            ToppingID = topping.ID,
                            ToppingName = topping.Name,
                            ToppingPrice = topping.Price
                        };
            return query;
        }

        public ActionResult AddToCart
            (
            int productID,
            int? quantity,
            string sizeID,
            string amountOfStone,
            string amountOfSugar,
            Topping[] topping
            )
        {
            double? TotalAmount = 0;
            if (Session["CartItems"] == null)
            {
                Session["CartItems"] = new List<CartItem>();
            }

            cartItems = GetListCart();
            if (cartItems.FirstOrDefault(m => m.ProductID == productID) == null)
            {
                Product sp = db.Products.Find(productID);
                Size size = db.Sizes.Find(sizeID);
                foreach (var item in topping)
                {
                    TotalAmount += item.Price;
                }
                CartItem newItem = new CartItem()
                {
                    ProductID = sp.ProductID,
                    ProductName = sp.ProductName,
                    Images = sp.Image,
                    Price = sp.Price,
                    Quantity = quantity,
                    MetaTitle = sp.MetaTitle,
                    AmountOfStone = amountOfStone,
                    AmountOfSugar = amountOfSugar,
                    SizeID = size.SizeID,
                    UnitPrice = size.UnitPrice,
                    ToppingID = topping,
                    Totals = quantity * (sp.Price + size.UnitPrice + TotalAmount)
                };

                cartItems.Add(newItem);
                messages.Success = true;
                messages.Contents = "Thêm sản phẩm vào giỏ hàng thành công !";
                return Content(JsonConvert.SerializeObject(new
                {
                    messages
                }));
            }
            else
            {
                CartItem cardItem = cartItems.FirstOrDefault(s => s.ProductID == productID);
                Size size = db.Sizes.Find(sizeID);
                foreach (var item in topping)
                {
                    TotalAmount += item.Price;
                }
                CartItem newItem = new CartItem()
                {
                    ProductID = cardItem.ProductID,
                    ProductName = cardItem.ProductName,
                    Images = cardItem.Images,
                    MetaTitle = cardItem.MetaTitle,
                    Price = cardItem.Price,
                    Quantity = quantity,
                    AmountOfStone = amountOfStone,
                    AmountOfSugar = amountOfSugar,
                    SizeID = size.SizeID,
                    UnitPrice = cardItem.UnitPrice,
                    ToppingID = topping,
                    Totals = quantity * (cardItem.Price + size.UnitPrice + TotalAmount)
                };
                cartItems.Add(newItem);
                messages.Success = true;
                messages.Contents = "Thêm sản phẩm vào giỏ hàng thành công !";
                return Content(JsonConvert.SerializeObject(new
                {
                    messages
                }));
            }
            messages.Success = false;
            messages.Contents = "Thêm sản phẩm vào giỏ hàng thất bại !";
            return Content(JsonConvert.SerializeObject(new
            {
                messages
            }));
            //return null;
        }
        public ActionResult DeleteCartItem(int productID)
        {
            cartItems = GetListCart();
            CartItem deleteItems = cartItems.FirstOrDefault(m => m.ProductID == productID);
            if (deleteItems != null)
            {
                cartItems.Remove(deleteItems);

                messages.Success = true;
                messages.Contents = "Xóa sản phẩm thành công!";
                return Content(JsonConvert.SerializeObject(new
                {
                    messages
                }));
            }
            else
            {
                messages.Success = false;
                messages.Contents = "Không tìm thấy sản phẩm trong giỏ!";
                return Content(JsonConvert.SerializeObject(new
                {
                    messages
                }));
            }

        }

        public ActionResult ChangeCartItem(int productID,int quantity)
        {
            cartItems = GetListCart();
            CartItem editItems = cartItems.FirstOrDefault(m => m.ProductID == productID);
            if (editItems!=null)
            {
                editItems.Quantity = quantity;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                cartItems
            }));
        }
    }
}