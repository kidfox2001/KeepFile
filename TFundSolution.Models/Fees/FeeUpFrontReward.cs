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

    [Table("CIS.FEE_UPFRONT_REWARD")]
    public class FeeUpFrontReward
    {
        public FeeUpFrontReward()
        {
            this.FUR_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }

        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [StringLength(36)]
        public string FUR_ID { get; set; }

        [ForeignKey("OfUpFront")]
        [StringLength(36)]
        public string FUM_ID { get; set; }
        public virtual FeeUpFrontMarketing OfUpFront { get; set; }

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
            this.FEE_BY_LEVEL = ((decimal)(this.OfUpFront.FEE_ONPAY * (decimal)this.RATE_USED)).WithoutRounding();

            return this.FEE_BY_LEVEL;
        }

        #endregion


    }

}
