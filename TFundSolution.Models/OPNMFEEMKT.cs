namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CIS.OPNMFEEMKT")]
    public partial class OPNMFEEMKT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string FUND { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string AGENT { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4)]
        public string MKT { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime DATE_NAV { get; set; }

        public decimal? FEE_AMT { get; set; }

        public decimal? TOT_UNI { get; set; }

        public decimal? MKT_UNI { get; set; }

        public decimal? MKT_FEE { get; set; }

        [StringLength(10)]
        public string UPD_BY { get; set; }

        public DateTime? UPD_DATE { get; set; }

        public decimal? BF_MKTUNIT { get; set; }

        public decimal? PER_UNIT { get; set; }

        public decimal? PER_FEE { get; set; }

        public decimal? STAFF_UNI { get; set; }

        public decimal? STAFF_AMT { get; set; }

        public decimal? STAFF_FEE { get; set; }

        public decimal? MKT_PAY { get; set; }

        public decimal? OLD_MKTUNIT { get; set; }

        public decimal? OLD_MKTPAY { get; set; }

        public decimal? MKT_UNIY1 { get; set; }

        public decimal? MKT_UNIY2 { get; set; }

        public decimal? MKT_UNIY3 { get; set; }

        public decimal? LEVEL1_PAY { get; set; }

        public decimal? LEVEL2_PAY { get; set; }

        public decimal? LEVEL3_PAY { get; set; }

        public decimal? LEVEL1_PAY2 { get; set; }

        public decimal? LEVEL2_PAY2 { get; set; }

        public decimal? LEVEL3_PAY2 { get; set; }

        public decimal? LEVEL1_PAY3 { get; set; }

        public decimal? LEVEL2_PAY3 { get; set; }

        public decimal? LEVEL3_PAY3 { get; set; }

        public decimal? MKT_UNI_NEW { get; set; }

        public decimal? MKT_FEE_NEW { get; set; }
    }
}
