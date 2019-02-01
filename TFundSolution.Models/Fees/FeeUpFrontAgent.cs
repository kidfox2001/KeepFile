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

    [Table("CIS.FEE_UPFRONT_AGENT")]
    public class FeeUpFrontAgent
    {

        public FeeUpFrontAgent()
        {
            this.FUA_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
            this.IS_PAID = true;
        }

        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [StringLength(36)]
        public string FUA_ID { get; set; }

        [ForeignKey("OnDateAgentFee")]
        [StringLength(36)]
        public string FAD_ID { get; set; }
        public virtual FeeAgentDaily OnDateAgentFee { get; set; }

        public decimal RATE_USED { get; set; }

        public decimal TOTAL_MONEY { get; set; }

        public decimal FEE { get; set; }

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

        #region Method

        public decimal CalculateFee()
        {
           var setting = this.OnDateAgentFee.SettingOwner;

           if (setting != null)
           {
               var settingUpFront = setting.GetUpFrontSettingByCondition( this.OnDateAgentFee.FEE_DATE,this.CHANNEL);
               if (settingUpFront != null)
               {
                   this.RATE_USED = settingUpFront.RateAgentCalculated;
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

        #endregion

    }
}
