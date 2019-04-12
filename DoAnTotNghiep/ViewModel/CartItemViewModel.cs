using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnTotNghiep.ViewModel
{
    public class CartItemViewModel
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
        public string AmountOfStone { get; set; }

        /// <summary>
        /// Lượng đường
        /// </summary>
        public string AmountOfSugar { get; set; }

        /// <summary>
        /// Mã size
        /// </summary>
        public string SizeID { get; set; }

        /// <summary>
        /// Đơn giá theo size
        /// </summary>
        public double? UnitPrice { get; set; }

        /// <summary>
        /// Mã topping
        /// </summary>
        public int ToppingID { get; set; }

        /// <summary>
        /// Tên topping
        /// </summary>
        public string ToppingName { get; set; }

        /// <summary>
        /// Giá topping
        /// </summary>
        public double? ToppingPrice { get; set; }

        /// <summary>
        /// Thành tiền
        /// </summary>
        public double? Totals
        {
            get
            {
                return (Quantity * Price) + UnitPrice + ToppingPrice;
            }
        }
    }
}