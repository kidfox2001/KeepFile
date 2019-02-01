namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CIS.OPNMFEEONPAY")]
    public partial class OPNMFEEONPAY
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string FUND { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(9)]
        public string BR_CODE { get; set; }

        public decimal? ON_PAYMKT { get; set; }

        public decimal? ON_PAYAGENT { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime START_DATE { get; set; }

        public DateTime? END_DATE { get; set; }

        public DateTime? UPD_DATE { get; set; }

        [StringLength(20)]
        public string UPD_BY { get; set; }
    }
}
