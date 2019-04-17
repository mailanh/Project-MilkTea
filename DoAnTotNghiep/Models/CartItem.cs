using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnTotNghiep.Models
{
    public class CartItem
    {
        /// <summary>
        /// Mã sản phẩm
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Ảnh mô tả
        /// </summary>
        public string Images { get; set; }

        public string MetaTitle { get; set; }

        /// <summary>
        /// Đơn giá sản phẩm
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Số lượng Order
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// Lượng đá
        /// </summary>
        public string AmountOfStone { get; set; } = "30%";

        /// <summary>
        /// Lượng đường
        /// </summary>
        public string AmountOfSugar { get; set; } = "30%";

        /// <summary>
        /// Mã size
        /// </summary>
        public string SizeID { get; set; } = "M";

        /// <summary>
        /// Đơn giá theo size
        /// </summary>
        public double? UnitPrice { get; set; }

        /// <summary>
        /// Mã topping
        /// </summary>
        public Topping[] ToppingID { get; set; }
        public string[] ToppingID1 { get; set; }

        /// <summary>
        /// Thành tiền
        /// </summary>
        public double? Totals { get; set; }

        public double? PriceWithOption { get; set; }
        public int? index { get; set; }
    }
    public class InputCartItem
    {
        public int productID { get; set; }
        public int? quantity { get; set; }
        public string sizeID { get; set; }
        public string amountOfStone { get; set; }
        public string amountOfSugar { get; set; }
        public string[] Topping1 { get; set; }
        public int? index { get; set; }
    }
}