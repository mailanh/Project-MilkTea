namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Topping")]
    public partial class Topping
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public double? Price { get; set; }

        public bool? Status { get; set; }
    }
}
