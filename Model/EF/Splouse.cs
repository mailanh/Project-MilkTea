namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Splouse
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? Quantity { get; set; }

        [StringLength(50)]
        public string UnitName { get; set; }

        public double? Price { get; set; }

        public bool? Status { get; set; }
    }
}
