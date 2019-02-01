namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CIS.OPNTTRAN")]
    public partial class OPNTTRAN
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RUN_NO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string TRAN_FLG { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string FUND_CODE { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string CIS_NO { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(7)]
        public string HOLDER_ID { get; set; }

        public DateTime? TRAN_DATE { get; set; }

        public DateTime? ISSUE_DATE { get; set; }

        public decimal? SHARE_AMT { get; set; }

        public decimal? BENEFIT_AMT { get; set; }

        public decimal? SHARE_UNIT { get; set; }

        [StringLength(20)]
        public string COMP_RECV { get; set; }

        [StringLength(30)]
        public string FUND_RECV { get; set; }

        public decimal? NAV_AVG { get; set; }

        [StringLength(100)]
        public string ACCT_NAME { get; set; }

        [StringLength(10)]
        public string BR_CODE_OLD { get; set; }

        [StringLength(4)]
        public string MKT_CODE_OLD { get; set; }

        [StringLength(10)]
        public string BR_CODE { get; set; }

        [StringLength(4)]
        public string MKT_CODE { get; set; }

        public decimal? TOT_SHARE { get; set; }

        [StringLength(1)]
        public string FLAG_UP { get; set; }

        [StringLength(10)]
        public string UPD_BY { get; set; }

        public DateTime? UPD_DATE { get; set; }

        [StringLength(10)]
        public string CER_NO { get; set; }

        [StringLength(10)]
        public string CIS_TRAN { get; set; }

        [StringLength(8)]
        public string ORDER_NO { get; set; }

        public decimal? TAX_BENEFIT { get; set; }

        public decimal? REAL_COST { get; set; }

        public decimal? REAL_BENEFIT { get; set; }

        [StringLength(1)]
        public string SELL_CONDITION { get; set; }
    }
}
