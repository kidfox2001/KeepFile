namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CIS.V_W_NAV")]
    public partial class DailyNav
    {
        [Key]
        [ForeignKey("FundOwner")]
        [Column(Order = 0)]
        [StringLength(20)]
        public string CLIENT_CODE { get; set; }
        public Fund FundOwner { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime DATA_DATE { get; set; }

        [StringLength(10)]
        public string TYPE { get; set; }

        [StringLength(10)]
        public string SUBTYPE { get; set; }

        [Column(TypeName = "float")]
        public decimal? ISSUESTOCK { get; set; }

        [Column(TypeName = "float")]
        public decimal? NAV { get; set; }

        [Column(TypeName = "float")]
        public decimal? BID { get; set; }

        [Column(TypeName = "float")]
        public decimal? OFFER { get; set; }

        [Column(TypeName = "float")]
        public decimal? NAV_AMT { get; set; }

        [Column(TypeName = "float")]
        public decimal? BID30 { get; set; }

        [Column(TypeName = "float")]
        public decimal? BID90 { get; set; }
    }
}
