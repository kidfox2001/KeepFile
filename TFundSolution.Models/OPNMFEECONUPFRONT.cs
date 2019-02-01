namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CIS.OPNMFEECONUPFRONT")]
    public partial class OPNMFEECONUPFRONT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string FUND { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(9)]
        public string BR_CODE { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(15)]
        public string ORDER_TYPE { get; set; }

        public decimal? UPFRONT_FEE { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime START_DATE { get; set; }

        public DateTime? END_DATE { get; set; }

        public DateTime? UPD_DATE { get; set; }

        [StringLength(20)]
        public string UPD_BY { get; set; }

        public decimal? UPFRONT_AGENT { get; set; }

        public decimal? UPFRONT_MKT { get; set; }
    }
}
