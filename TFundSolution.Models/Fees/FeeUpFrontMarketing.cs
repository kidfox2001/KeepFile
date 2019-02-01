using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFundSolution.Utils.Numbers;

namespace TFundSolution.Models
{
    [Table("CIS.FEE_UPFRONT_MKT")]
    public class FeeUpFrontMarketing
    {

        public FeeUpFrontMarketing()
        {
            this.FUM_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }

        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [StringLength(36)]
        public string FUM_ID { get; set; }

        [ForeignKey("OfMarketingFee")]
        [StringLength(36)]
        public string FEM_ID { get; set; }
        public virtual FeeMarketing OfMarketingFee { get; set; }

        public decimal RATE_USED { get; set; }

        public decimal TOTAL_MONEY { get; set; }

        public decimal? TOTAL_FEE_REWARD { get; set; }

        public decimal FEE { get; set; }

        public decimal? FEE_ONPAY { get; set; }

        [StringLength(15)]
        public string CHANNEL { get; set; }

        public bool IS_T_WEALTH { get; set; }

        public bool IS_PAID { get; set; }

        [StringLength(20)]
        public string UPDATE_BY { get; set; }

        private DateTime? _UPDATE_DATE;
        public DateTime? UPDATE_DATE
        {
            get
            {
                return _UPDATE_DATE ?? DateTime.Now;
            }
            set
            {
                _UPDATE_DATE = value;
            }
        }

        [NotMapped]
        public EnumDataStatus? DataStatus { get; set; }

        [NotMapped]
        public string TWEALTH
        {
            set
            {
                if (value != null && value.ToUpper() == "TWEALTH".ToUpper())
                {
                    this.IS_T_WEALTH = true;
                }
                else
                {
                    this.IS_T_WEALTH = false;
                }
            }
        }

        [NotMapped]
        public string TWEALTH_PAY
        {
            set
            {
                this.IS_PAID = true;
                if (value != null && this.IS_T_WEALTH && value.ToUpper() == "F".ToUpper())
                {
                    this.IS_PAID = false;
                }
            }
        }

        #region Relation

        private ICollection<FeeUpFrontReward> _OnLevelDetails = new HashSet<FeeUpFrontReward>();
        public virtual ICollection<FeeUpFrontReward> OnLevelDetails
        {
            get { return _OnLevelDetails; }
            set { _OnLevelDetails = value; }
        }

        #endregion

        #region Method


        public FeeUpFrontReward NewLevelDetail(FEE_SETTING_UPFRONT_ONLEVEL settingLevel)
        {
            var newLevel = new FeeUpFrontReward();
            newLevel.FUM_ID = this.FUM_ID;
            newLevel.OfUpFront = this;
            newLevel.LEVEL = settingLevel.LEVEL;
            newLevel.RATE_USED = settingLevel.RateCalculated;

            this.OnLevelDetails.Add(newLevel);


            return newLevel;
        }

        public void ReLevelFromSetting()
        {
            if (this.IS_PAID)
            {
                var setting = this.OfMarketingFee.OnDateAgentFee.SettingOwner;
                if (setting != null)
                {
                    var settingOnlevels = ((FEE_SETTING_TBANK)setting).GetUpFrontOnLevel();

                    foreach (var itemSetting in settingOnlevels)
                    {
                        this.NewLevelDetail(itemSetting);
                    }
                }
            }

        }


        /// <summary>
        /// คำนวนค่า fee up front
        /// </summary>
        /// <returns></returns>
        public decimal CalculateFee()
        {
            var setting = this.OfMarketingFee.OnDateAgentFee.SettingOwner;

            if (setting != null)
            {
                FEE_SETTING_UPFRONT_CHANNEL settingUpFront = setting.GetUpFrontSettingByCondition(this.OfMarketingFee.FEE_DATE, this.CHANNEL);
                if (settingUpFront != null)
                {
                    this.RATE_USED = settingUpFront.RateMktCalculated;
                    this.FEE = ((decimal)(this.TOTAL_MONEY * this.RATE_USED)).WithoutRounding();
                }
                else
                {
                    // ไม่เจอการตั้งค่า ไม่มีค่า fee 
                    this.FEE = 0;
                }
            }

            return this.FEE;
        }

        public decimal CalculateFeeOnpay()
        {
            if (this.IS_PAID)
            {
                if (this.IS_T_WEALTH)
                {
                    // แบบเป้นลูกค้า TWealth
                    this.FEE_ONPAY = (this.FEE * (decimal)this.OfMarketingFee.RATE_UPFRONT_ONPAY_USED
                                              * (decimal)this.OfMarketingFee.RATE_UPFRONT_TWEALTH_USED).WithoutRounding();
                }
                else
                {
                    // แบบ ธรรมดา
                    this.FEE_ONPAY = (this.FEE * (decimal)this.OfMarketingFee.RATE_UPFRONT_ONPAY_USED).WithoutRounding();
                }

            }

            return this.FEE_ONPAY ?? 0m;
        }

        public decimal CalculateFeeReward()
        {
            if (this.IS_PAID)
            {
                this.TOTAL_FEE_REWARD = this.OnLevelDetails.Sum(m => m.CalculateFee());
            }

            return this.TOTAL_FEE_REWARD ?? 0m;
        }

        #endregion

    }
}
