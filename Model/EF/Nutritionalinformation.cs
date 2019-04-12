namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Nutritionalinformation")]
    public partial class Nutritionalinformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [StringLength(10)]
        public string Fat { get; set; }

        [StringLength(10)]
        public string Energy { get; set; }

        [StringLength(10)]
        public string VitaminB1 { get; set; }

        [StringLength(10)]
        public string Calo { get; set; }

        [StringLength(10)]
        public string Canxi { get; set; }
    }
}
