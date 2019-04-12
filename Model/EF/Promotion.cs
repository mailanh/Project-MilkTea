namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Promotion")]
    public partial class Promotion
    {
        public int PromotionID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? StartEnd { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [StringLength(100)]
        public string PromotionName { get; set; }

        [StringLength(100)]
        public string MetaTitle { get; set; }

        [StringLength(50)]
        public string PromotionImage { get; set; }

        public bool? Status { get; set; }
    }
}
