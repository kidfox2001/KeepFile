namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CIS.OPNMFEEREWARD")]
    public partial class OPNMFEEREWARD
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string AGENT { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string FEE_TYPE { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(1)]
        public string FEE_PERIOD { get; set; }

        public decimal? PER_LEVEL1 { get; set; }

        public decimal? PER_LEVEL2 { get; set; }

        public decimal? PER_LEVEL3 { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime START_DATE { get; set; }

        public DateTime? END_DATE { get; set; }

        public DateTime? UPD_DATE { get; set; }

        [StringLength(20)]
        public string UPD_BY { get; set; }
    }
}
