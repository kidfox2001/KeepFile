namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("CIS.OPNMAGEN")]
    public partial class Agent
    {
        [Key]
        [StringLength(9)]
        public string BR_CODE { get; set; }

        [Required]
        [StringLength(100)]
        public string BR_NAME { get; set; }

        [StringLength(50)]
        public string BR_ADDR1 { get; set; }

        [StringLength(50)]
        public string BR_ADDR2 { get; set; }

        [StringLength(50)]
        public string BR_ADDR3 { get; set; }

        [StringLength(5)]
        public string BR_ZIP { get; set; }

        [StringLength(50)]
        public string BR_PROV { get; set; }

        [StringLength(8)]
        public string NEW_ORDER { get; set; }

        [StringLength(10)]
        public string NEW_CISNO { get; set; }

        [StringLength(2)]
        public string GROUP_FUND { get; set; }

        [StringLength(10)]
        public string NEW_ORDER_IPO { get; set; }

        [StringLength(16)]
        public string REGIS_NO { get; set; }

        [StringLength(5)]
        public string REGIS_TYPE { get; set; }

        [StringLength(1)]
        public string BR_FLAG { get; set; }

        public string AgentCode 
        {
            get
            {
                if (String.IsNullOrEmpty(this.BR_CODE))
                {
                    return "";
                }
                else
                {
                    return this.BR_CODE.Substring(0, 3);
                }
            }
        }

        public bool IsTBank
        {
            get
            {
                if (BR_CODE != null)
                {
                    if (BR_CODE.Substring(0, 3).ToUpper() == "EFS".ToUpper())
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        #region Relation

        private ICollection<FEE_SETTING> _FeeSettings = new HashSet<FEE_SETTING>();
        public virtual ICollection<FEE_SETTING> SettingFees
        {
            get { return _FeeSettings; }
            set { _FeeSettings = value; }
        }

        private ICollection<FeeAgentDaily> _FeeDailys = new HashSet<FeeAgentDaily>();
        public virtual ICollection<FeeAgentDaily> FeeDailys
        {
            get { return _FeeDailys; }
            set { _FeeDailys = value; }
        }

        private ICollection<AgentBfValue> _HaveBfValueWithFund = new HashSet<AgentBfValue>();
        public virtual ICollection<AgentBfValue> HaveBfValueWithFunds
        {
            get { return _HaveBfValueWithFund; }
            set { _HaveBfValueWithFund = value; }
        }

        //private ICollection<MarketingBfValue> _HaveBfValueWithMkt = new HashSet<MarketingBfValue>();
        //public virtual ICollection<MarketingBfValue > HaveBfValueWithMkt
        //{
        //    get { return _HaveBfValueWithMkt; }
        //    set { _HaveBfValueWithMkt = value; }
        //}

        #endregion

        #region Method

        public FeeAgentDaily GetFeeAgentByDate(DateTime feeDate)
        {
            return this.FeeDailys.SingleOrDefault(q => q.FEE_DATE == feeDate);
        }

        //public FEE_SETTING GetSettingByFund(string fundCode)
        //{
        //    return this.SettingFees.SingleOrDefault(m => m.FUND_ID == fundCode);
        //}


        /// <summary>
        /// เรียกข้อมูลค่า bfrmf ของ agent และ fund ถ้าไม่พบจะส่งค่า 0
        /// </summary>
        /// <param name="fundCode"></param>
        /// <returns></returns>
        public decimal GetAgentBfValueByFund(string fundCode)
        {
            var bfValue = this.HaveBfValueWithFunds.SingleOrDefault(m => m.FUND_ID == fundCode);
            return bfValue == null ? 0 : bfValue.BF_VALUE;
        }

        /// <summary>
        /// เรียกข้อมูล entity bfrmf ถ้าไม่พบคืนค่า null
        /// </summary>
        /// <param name="fundCode"></param>
        /// <returns></returns>
        public AgentBfValue GetAgentBfByFund(string fundCode)
        {
            return this.HaveBfValueWithFunds.SingleOrDefault(m => m.FUND_ID == fundCode);
        }

        //public decimal GetMarketingBfValueByFundAndMkt(string fundCode, string mktCode)
        //{
        //    var bfValue = this.HaveBfValueWithMkt.SingleOrDefault(m => m.FUND_ID == fundCode & m.MKT == mktCode);
        //    return bfValue == null ? 0 : bfValue.BF_VALUE;
        //}

        public FeeAgentDaily AddFeeDaily(string fundCode, DateTime feeDate, DateTime feeBeforeDate)
        {
            FeeAgentDaily newFeeDate = new FeeAgentDaily()
            {
                AGENT_ID = this.BR_CODE,
                FUND_ID = fundCode,
                FEE_DATE = feeDate,
                FEE_BEFORE_DATE = feeBeforeDate
            };

            this.FeeDailys.Add(newFeeDate);

            return newFeeDate;
        }

        public FEE_SETTING NewSetting(string fund)
        {

            if (this.IsTBank)
            {
                return new FEE_SETTING_TBANK() { 
                    FUND_ID = fund, 
                    AGENT_ID = this.BR_CODE ,
                    DataStatus = EnumDataStatus.NewData 
                };
            }
            else 
            {

                return new FEE_SETTING() { 
                    FUND_ID = fund,
                    AGENT_ID = this.BR_CODE,
                    DataStatus = EnumDataStatus.NewData };
            }

        }

        #endregion

    }
}
