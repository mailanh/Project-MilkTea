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

        public ActionResult AddToCart(InputCartItem input)
        {
            double? TotalAmount = 0;
            if (Session["CartItems"] == null)
            {
                Session["CartItems"] = new List<CartItem>();
            }

            cartItems = GetListCart();
            int count = cartItems.Count();
            int index = 0;
            if (cartItems.FirstOrDefault(m => m.ProductID == input.productID) == null)
            {
                if (count != 0)
                {
                    index = count++;
                }
                else
                {
                    index = 1;
                }
                Product sp = db.Products.Find(input.productID);
                Size size = db.Sizes.Find(input.sizeID);
                if (input.Topping1 != null)
                {
                    for (int i = 0; i < input.Topping1.Length; i++)
                    {
                        string ToppingName = input.Topping1[i];
                        Topping tp = db.Toppings.FirstOrDefault(s => s.Name == ToppingName);
                        TotalAmount += tp.Price;
                    }
                }

                //Topping tp = db.Toppings.Find(1);

                //if (topping != null)
                //{
                //    foreach (var item in topping)
                //    {
                //        TotalAmount += item.Price;
                //    }
                //}
                CartItem newItem = new CartItem()
                {
                    ProductID = sp.ProductID,
                    ProductName = sp.ProductName,
                    Images = sp.Image,
                    Price = sp.Price,
                    Quantity = input.quantity,
                    MetaTitle = sp.MetaTitle,
                    AmountOfStone = input.amountOfStone,
                    AmountOfSugar = input.amountOfSugar,
                    SizeID = size.SizeID,
                    UnitPrice = size.UnitPrice,
                    //ToppingID = topping,
                    ToppingID1 = input.Topping1,
                    index = index,
                    PriceWithOption = (sp.Price + size.UnitPrice + TotalAmount),
                    Totals = input.quantity * (sp.Price + size.UnitPrice + TotalAmount),
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

                CartItem cardItem = cartItems.FirstOrDefault(s => s.ProductID == input.productID);
                Size size = db.Sizes.Find(input.sizeID);
                if (input.Topping1 != null)
                {
                    for (int i = 0; i < input.Topping1.Length; i++)
                    {
                        string ToppingName = input.Topping1[i];
                        Topping tp = db.Toppings.FirstOrDefault(s => s.Name == ToppingName);
                        TotalAmount += tp.Price;
                    }
                }

                //Size size = db.Sizes.Find(sizeID);
                //if (topping != null) { 
                //        foreach (var item in topping)
                //        {
                //            TotalAmount += item.Price;
                //        }
                //}
                CartItem newItem = new CartItem()
                {
                    ProductID = cardItem.ProductID,
                    ProductName = cardItem.ProductName,
                    Images = cardItem.Images,
                    MetaTitle = cardItem.MetaTitle,
                    Price = cardItem.Price,
                    Quantity = input.quantity,
                    AmountOfStone = input.amountOfStone,
                    AmountOfSugar = input.amountOfSugar,
                    SizeID = size.SizeID,
                    UnitPrice = cardItem.UnitPrice,
                    //ToppingID = topping,
                    index = index,
                    PriceWithOption = (cardItem.Price + size.UnitPrice + TotalAmount),
                    Totals = input.quantity * (cardItem.Price + size.UnitPrice + TotalAmount)
                };
                cartItems.Add(newItem);
                messages.Success = true;
                messages.Contents = "Thêm sản phẩm vào giỏ hàng thành công !";
                return Content(JsonConvert.SerializeObject(new
                {
                    messages
                }));
            }
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

        public ActionResult ChangeCartItem(int index, int quantity)
        {
            cartItems = GetListCart();
            CartItem editItems = cartItems.FirstOrDefault(m => m.index == index);
            if (editItems != null)
            {
                editItems.Quantity = quantity;
                editItems.Totals = editItems.Quantity * editItems.PriceWithOption;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                cartItems
            }));
        }

        public ActionResult GetCartModal(int productID)
        {
            cartItems = GetListCart();
            CartItem editCart = cartItems.FirstOrDefault(m => m.ProductID == productID);
            var listSize = db.Sizes.ToList();
            var listStone = db.AmountOfStones.ToList();
            var listSugar = db.AmountOfSugars.ToList();
            var listTopping = db.Toppings.ToList();
            return Content(JsonConvert.SerializeObject(new
            {
                editCart,
                listSize,
                listStone,
                listSugar,
                listTopping,
            }));
        }

        public ActionResult EditCart(InputCartItem input)
        {
            double? TotalAmount = 0;
            cartItems = GetListCart();
            CartItem editCart = cartItems.FirstOrDefault(m => m.index == input.index);
            Size size = db.Sizes.FirstOrDefault(s => s.SizeID == input.sizeID);
            editCart.Quantity = input.quantity;
            editCart.AmountOfStone = input.amountOfStone;
            editCart.AmountOfSugar = input.amountOfSugar;
            editCart.SizeID = input.sizeID;
            editCart.ToppingID1 = input.Topping1;
            var a = editCart.ToppingID1;
            if (a != null)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    string ToppingName = a[i];
                    Topping tp = db.Toppings.FirstOrDefault(s => s.Name == ToppingName);
                    TotalAmount += tp.Price;

                }
            }
            editCart.PriceWithOption = (editCart.Price + size.UnitPrice + TotalAmount);
            editCart.Totals = input.quantity * (editCart.Price + size.UnitPrice + TotalAmount);

            messages.Success = true;
            messages.Contents = "Cập nhật giỏ hàng thàng thành công !";
            return Content(JsonConvert.SerializeObject(new
            {
                messages
            }));
        }
    }
}