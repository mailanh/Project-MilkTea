namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int ID { get; set; }

        [StringLength(150)]
        public string Images { get; set; }

        public int? Displayorder { get; set; }

        [StringLength(150)]
        public string Link { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public bool? Status { get; set; }
    }
}
