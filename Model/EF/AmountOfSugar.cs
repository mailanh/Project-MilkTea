namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AmountOfSugar")]
    public partial class AmountOfSugar
    {
        [Key]
        [StringLength(10)]
        public string AmountSugarName { get; set; }
    }
}
