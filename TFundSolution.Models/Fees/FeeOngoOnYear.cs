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

    [Table("CIS.FEE_ONGO_YEAR")]
    public class FeeOngoOnYear
    {
        public FeeOngoOnYear()
        {
            this.FUY_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }

        [Key]
        [StringLength(36)]
        public string FUY_ID { get; set; }

        [ForeignKey("OfMarketingFee")]
        [StringLength(36)]
        public string FEM_ID { get; set; }
        public virtual FeeMarketing OfMarketingFee { get; set; }

        [StringLength(2)]
        public string YEAR_ORDER { get; set; }

        public decimal UNIT_BY_YEAR { get; set; }

        public decimal FEE_BY_YEAR { get; set; }

        public decimal TOTAL_FEE_BY_LEVEL { get; set; }

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
        public decimal RateLevelOne { get; set; }

        [NotMapped]
        public bool IsYearZero
        {
            get
            {
                if (this.YEAR_ORDER.ToUpper() == "B".ToUpper())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [NotMapped]
        public EnumDataStatus? DataStatus { get; set; }

        
        #region Relation

        private ICollection<FeeOnGoReward> _OnLevelDetails = new HashSet<FeeOnGoReward>();
        public virtual ICollection<FeeOnGoReward> OnLevelDetails
        {
            get { return _OnLevelDetails; }
            set { _OnLevelDetails = value; }
        }

        #endregion

        #region Method

        public FeeOnGoReward GetRewardDetailByLevel(int onLevel)
        {
            FeeOnGoReward findReward = this.OnLevelDetails.SingleOrDefault(m => m.LEVEL == onLevel) ?? new FeeOnGoReward();

            return findReward;
        }

        /// <summary>
        /// คำนวนค่า fee ระดับ year ก่อนโดยควรคำนวน onpay มาแล้ว และควร sum unit ongo มาด้วย
        /// </summary>
        /// <returns></returns>
        public decimal CalculateFee()
        {
            if (((decimal)this.OfMarketingFee.TOTAL_UNIT_ONGO) > 0)
            {
                this.FEE_BY_YEAR = ((this.UNIT_BY_YEAR * (decimal)this.OfMarketingFee.TOTAL_FEE_ONGO_ONPAY) / (decimal)this.OfMarketingFee.TOTAL_UNIT_ONGO).WithoutRounding();
            }
            else
            {
                this.FEE_BY_YEAR = 0;
            }
           
            return this.FEE_BY_YEAR;
        }

        public decimal CalculateFeeReward()
        {
            this.TOTAL_FEE_BY_LEVEL = this.OnLevelDetails.Sum(m => m.CalculateFee());
            return this.TOTAL_FEE_BY_LEVEL;
        }

        /// <summary>
        /// ปรับ level ที่มีจากการตั้งค่าเข้าไปในปี รวม rate ด้วย
        /// </summary>
        /// <param name="settings"></param>
        public void ReLevelFromSetting()
        {
            var setting = this.OfMarketingFee.OnDateAgentFee.SettingOwner;
            if (setting != null)
            {
                var settingYear = ((FEE_SETTING_TBANK)setting).GetOngoOnYearSettingByYear(this.YEAR_ORDER);

                foreach (var itemSetting in settingYear.SettingOnLevels)
                {
                    FeeOnGoReward newItem = new FeeOnGoReward()
                       {
                           FUY_ID = this.FUY_ID,
                           OnYear = this,
                           LEVEL = itemSetting.LEVEL,
                           RATE_USED = itemSetting.RateCalculated
                       };

                    this.OnLevelDetails.Add(newItem);

                    if (itemSetting.LEVEL == 1)
                    {
                        this.RateLevelOne = itemSetting.RateCalculated;
                    }
                }
            }
        }

        #endregion

    }
}
