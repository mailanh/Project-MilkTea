namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AmountOfStone")]
    public partial class AmountOfStone
    {
        [Key]
        [StringLength(10)]
        public string AmountStoneName { get; set; }
    }
}
