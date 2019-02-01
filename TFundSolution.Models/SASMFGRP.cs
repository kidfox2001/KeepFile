namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SAS.SASMFGRP")]
    public partial class SASMFGRP
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
        [StringLength(2)]
        public string GROUP_FUND { get; set; }

        public decimal? EXIT_FEE { get; set; }

        [StringLength(10)]
        public string ORDER_SB { get; set; }

        [StringLength(10)]
        public string ORDER_RD { get; set; }

        public decimal? ON_AGENTFEE { get; set; }

        public decimal? ON_MKTFEE { get; set; }

        public decimal? MGT_FEE { get; set; }

        [StringLength(10)]
        public string UPD_BY { get; set; }

        public DateTime? UPD_DATE { get; set; }

        [StringLength(4)]
        public string CHK_YEAR { get; set; }

        public DateTime? DATE_TO { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime DATE_FROM { get; set; }

        public byte? NO_PERIOD_EXIT { get; set; }

        public decimal? MIN_FEEAMT_EXIT { get; set; }

        public decimal? ONGO_TIER_RATE { get; set; }

        public decimal? ONGO_NEW_RATE { get; set; }

        public decimal? EXIT_NEW_RATE { get; set; }
    }
}
