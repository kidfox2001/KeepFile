namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("CIS.OPNCONTL")]
    public partial class Fund
    {
        [Key]
        [StringLength(50)]
        public string FUND { get; set; }


        [StringLength(100)]
        public string FUND_NAME { get; set; }

        [NotMapped]
        public bool IsExtraFund 
        { 
            get
            {
                if (this.FUND.ToUpper() == "T-VALUexUS".ToUpper() |
                    this.FUND.ToUpper() == "T-PREMIUM BRAND".ToUpper() |
                    this.FUND.ToUpper() == "T-NewEnergy".ToUpper() |
                    this.FUND.ToUpper() == "T-GlobalBond".ToUpper() |
                    this.FUND.ToUpper() == "T-GlobalValue".ToUpper() |
                    this.FUND.ToUpper() == "T-GlobalEnergy".ToUpper() |
                    this.FUND.ToUpper() == "T-LowBeta".ToUpper()
                    )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [StringLength(1)]
        public string FLG_NEW { get; set; }

        [StringLength(2)]
        public string CLASS_CODE { get; set; }

        [StringLength(2)]
        public string TYPE_CODE { get; set; }

        #region Relation

        private ICollection<FEE_SETTING> _FeeSettings = new HashSet<FEE_SETTING>();
        public virtual ICollection<FEE_SETTING> SettingFees
        {
            get { return _FeeSettings; }
            set { _FeeSettings = value; }
        }

        //private ICollection<FEE_SETTING_TBANK> _SettingFeeTBanks = new HashSet<FEE_SETTING_TBANK>();
        //public virtual ICollection<FEE_SETTING_TBANK> SettingFeeTBanks
        //{
        //    get { return _SettingFeeTBanks; }
        //    set { _SettingFeeTBanks = value; }
        //}

        private ICollection<DailyNav> _NavDates = new HashSet<DailyNav>();
        public virtual ICollection<DailyNav> NavDates
        {
            get { return _NavDates; }
            set { _NavDates = value; }
        }

        #endregion

        #region Method

        /// <summary>
        /// เรียกข้อมูลเฉพาะ ที่ไม่ใช่ agent id = 000 (เป็นการตั้งค่าแบบทุก agent)
        /// </summary>
        /// <returns></returns>
        public List<FEE_SETTING> GetFeeSettingsWithOutTemplate()
        {
            return this.SettingFees.Where(m => m.AGENT_ID != "000").ToList();
        }

        #endregion

        //[StringLength(7)]
        //public string HOLDER { get; set; }

        //[StringLength(10)]
        //public string CISNO { get; set; }

        //[StringLength(10)]
        //public string CERNO { get; set; }

        //public decimal? NAV { get; set; }

        //public DateTime? T_DATE { get; set; }


        //[StringLength(1)]
        //public string FLG_PRN { get; set; }

        //[StringLength(70)]
        //public string FUND_TYPE { get; set; }

        //public decimal? MIN_SHARE { get; set; }

        //[StringLength(2)]
        //public string FLG_CER { get; set; }

        //[StringLength(10)]
        //public string DOC_NO { get; set; }

        //[StringLength(70)]
        //public string BANK_NAME { get; set; }

        //[StringLength(150)]
        //public string BANK_ADDR { get; set; }

        //[StringLength(100)]
        //public string BANK_TEL { get; set; }

        //[StringLength(7)]
        //public string MOR_NO { get; set; }

        //public DateTime? SIGN_DATE { get; set; }

        //public decimal? SIGN_AMT { get; set; }

        //public decimal? BUY_MIN_SHARE { get; set; }

        //public decimal? SELL_MIN_SHARE { get; set; }

        //public decimal? BUY_NEXT { get; set; }

        //public decimal? SWITCH_OUT { get; set; }

        //public decimal? SWITCH_IN { get; set; }

        //public bool? DUE_DATE { get; set; }

        //public decimal? NAV_BUY { get; set; }

        //public decimal? NAV_SELL { get; set; }

        //[StringLength(8)]
        //public string UPD_BY { get; set; }

        //public DateTime? UPD_DATE { get; set; }

        //public decimal? SELL_MIN_AMT { get; set; }

        //public decimal? BUY_NEXT_BATH { get; set; }

        //public decimal? MIN_BATH { get; set; }

        //[StringLength(10)]
        //public string ORDER_SB { get; set; }

        //[StringLength(10)]
        //public string ORDER_RD { get; set; }

        //[StringLength(7)]
        //public string MATCH_NO { get; set; }

        //public DateTime? RESIGN_DATE { get; set; }

        //[StringLength(70)]
        //public string AUDIT_NAME { get; set; }

        //[StringLength(70)]
        //public string AUDIT_COMP { get; set; }

        //[StringLength(500)]
        //public string FUND_COND { get; set; }

        //[StringLength(500)]
        //public string FUND_POLICY { get; set; }

        //[StringLength(500)]
        //public string FUND_RETURN { get; set; }

        //public decimal? FEE_MANAGEMENT { get; set; }

        //public decimal? FEE_TRUSTEE { get; set; }

        //[StringLength(13)]
        //public string TAX_ID { get; set; }

        //public DateTime? IPO_START_DATE { get; set; }

        //public DateTime? IPO_END_DATE { get; set; }

        //[StringLength(2)]
        //public string REDM_FREQ { get; set; }

        //[StringLength(2)]
        //public string REDM_DAY_OF_WEEK { get; set; }

        //[StringLength(2)]
        //public string START_DAY_OF_MONTH { get; set; }

        //[StringLength(2)]
        //public string END_DAY_OF_MONTH { get; set; }

        //[StringLength(6)]
        //public string TRADE_START_TIME { get; set; }

        //[StringLength(6)]
        //public string TRADE_END_TIME { get; set; }

        //[StringLength(1)]
        //public string DIVIDENT_FREQ { get; set; }

        //[StringLength(2)]
        //public string BUY_FREQ { get; set; }

        //[StringLength(2)]
        //public string BUY_DAY_OF_WEEK { get; set; }

        //[StringLength(2)]
        //public string BSTART_DAY_OF_MONTH { get; set; }

        //[StringLength(2)]
        //public string BEND_DAY_OF_MONTH { get; set; }

        //public DateTime? ENTRAN_DATE { get; set; }

        //public decimal? BF_AMOUNT { get; set; }

        //[StringLength(50)]
        //public string MATURITY { get; set; }

        //[StringLength(15)]
        //public string MERCHANT_ID { get; set; }

        //public decimal? AR_MIN_BATH { get; set; }

        //[StringLength(1)]
        //public string SEND_WEB { get; set; }

        //[StringLength(1)]
        //public string AR_FLG { get; set; }

        //[StringLength(1)]
        //public string IVR_FLG { get; set; }

        //public decimal? AR_PERCENT { get; set; }

        //public byte? AR_NO_PERYEAR { get; set; }

        //public decimal? SW_NEXT_BATH { get; set; }

        //public decimal? SW_NEXT_SHARE { get; set; }

   

    }
}
