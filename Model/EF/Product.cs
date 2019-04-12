namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int ProductID { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }

        [StringLength(100)]
        public string MetaTitle { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(250)]
        public string MoreImages { get; set; }

        public double? PriceImport { get; set; }

        public double? Price { get; set; }

        public int? ViewCount { get; set; }

        public int? Quantity { get; set; }

        [StringLength(10)]
        public string AmountOfStoneID { get; set; }

        [StringLength(10)]
        public string AmountOfSugarID { get; set; }

        public int? ToppingID { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(5)]
        public string SizeID { get; set; }

        public int? SpliceID { get; set; }

        public int? SupplierID { get; set; }

        [StringLength(50)]
        public string IsHot { get; set; }

        [StringLength(50)]
        public string IsNew { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool? Status { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
