namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OPER.OPRHBUY")]
    public partial class OPRHBUY
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string FUND { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CIS_NO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(8)]
        public string ORDER_NO { get; set; }

        [StringLength(9)]
        public string BR_CODE { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime TRAN_DATE { get; set; }

        [StringLength(7)]
        public string HOLDER_ID { get; set; }

        public decimal? BUY_SHARE { get; set; }

        public decimal? BUY_MONEY { get; set; }

        public decimal? BF_SHARE { get; set; }

        public decimal? AL_SHARE { get; set; }

        public decimal? AL_MONEY { get; set; }

        [StringLength(3)]
        public string BUY_TYPE { get; set; }

        [StringLength(2)]
        public string FLAG { get; set; }

        [StringLength(9)]
        public string AGENT { get; set; }

        [StringLength(4)]
        public string MKT_CODE { get; set; }

        public decimal? NAV { get; set; }

        public decimal? FEE { get; set; }

        public decimal? VAT { get; set; }

        [StringLength(10)]
        public string CER_NO { get; set; }

        [StringLength(10)]
        public string DOC_NO { get; set; }

        [StringLength(1)]
        public string FLAG_P { get; set; }

        [StringLength(8)]
        public string UPD_BY { get; set; }

        public DateTime? UPD_DATE { get; set; }

        [StringLength(1)]
        public string FLAG_C { get; set; }

        [StringLength(1)]
        public string FLAG_UP { get; set; }

        [StringLength(1)]
        public string STATUS { get; set; }

        [StringLength(20)]
        public string FIRST_NAME { get; set; }

        [StringLength(90)]
        public string NAME { get; set; }

        [StringLength(90)]
        public string SURNAME { get; set; }

        [StringLength(6)]
        public string FLAG_R { get; set; }

        [StringLength(1)]
        public string ADDR_FLG { get; set; }

        [StringLength(5)]
        public string ZIP_CODE { get; set; }

        [StringLength(150)]
        public string ADDR1 { get; set; }

        [StringLength(70)]
        public string ADDR2 { get; set; }

        [StringLength(70)]
        public string ADDR3 { get; set; }

        public DateTime? DATE_R { get; set; }

        [StringLength(1)]
        public string FLAG_HOLD { get; set; }

        [StringLength(3)]
        public string SUBTYPE { get; set; }

        [StringLength(10)]
        public string FLAG_ERROR { get; set; }

        [StringLength(30)]
        public string FUND2 { get; set; }

        [StringLength(10)]
        public string ORDER_TYPE { get; set; }

        [StringLength(1)]
        public string FLAG_DOC { get; set; }

        [StringLength(2)]
        public string COLD_CALLING { get; set; }

        [StringLength(2)]
        public string EXEC_TYPE { get; set; }

        [StringLength(6)]
        public string IP_CODE { get; set; }

        [StringLength(1)]
        public string FLAG_INSURE { get; set; }
    }
}
