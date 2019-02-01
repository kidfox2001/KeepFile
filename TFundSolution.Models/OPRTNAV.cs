namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OPER.OPRTNAV")]
    public partial class OPRTNAV
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string FUND { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime NAV_DATE { get; set; }

        public decimal? NAV { get; set; }

        public decimal? NAV_PURCHASE { get; set; }

        public decimal? NAV_REDEM { get; set; }

        public decimal? NET_SHARE { get; set; }

        public decimal? NET_AMOUNT { get; set; }

        [StringLength(8)]
        public string UPD_BY { get; set; }

        public DateTime? UPD_DATE { get; set; }

        [StringLength(1)]
        public string FLAG_UPDATE { get; set; }

        [StringLength(1)]
        public string FLAG_LOAD { get; set; }

        public decimal? NAV30 { get; set; }

        public decimal? NAV90 { get; set; }

        [StringLength(1)]
        public string FLAG_UPDTYPE { get; set; }

        [StringLength(1)]
        public string NAV_TYPE { get; set; }

        public decimal? NAV_PURCHASE_SPEC { get; set; }

        public decimal? NAV_REDEM_SPEC { get; set; }

        public decimal? NAV_PURCHASE_BUYSPEC { get; set; }

        public DateTime? ANNOUNCE_DATE { get; set; }

        public decimal? NAV_PURCHASE_ELECTRONIC { get; set; }

        public decimal? NAV_REDEM_ELECTRONIC { get; set; }
    }
}
