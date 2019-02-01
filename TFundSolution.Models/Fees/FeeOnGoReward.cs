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
    [Table("CIS.FEE_ONGO_REWARD")]
    public class FeeOnGoReward
    {
        public FeeOnGoReward()
        {
            this.FRM_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }

        [Key]
        [StringLength(36)]
        public string FRM_ID { get; set; }

        [ForeignKey("OnYear")]
        [StringLength(36)]
        public string FUY_ID { get; set; }
        public virtual FeeOngoOnYear OnYear { get; set; }

        public int LEVEL { get; set; }

        public decimal FEE_BY_LEVEL { get; set; }

        public decimal? RATE_USED { get; set; }

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


        #region Method

        public decimal CalculateFee()
        {
            this.RATE_USED = this.RATE_USED ?? 0;

            if (this.LEVEL == 1)
            {
                this.FEE_BY_LEVEL = (this.OnYear.FEE_BY_YEAR * (decimal)this.RATE_USED).WithoutRounding();
            }
            else
            {
                // Level ที่มากกว่า หนึ่งจะต้องนำ สำอำส หนึ่งมาคูณก่อน
                this.FEE_BY_LEVEL = (this.OnYear.FEE_BY_YEAR * (decimal)this.OnYear.RateLevelOne * (decimal)this.RATE_USED).WithoutRounding();
            }

            return this.FEE_BY_LEVEL;
        }

        #endregion
    }
}
