using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models
{

    [Table("CIS.FEE_SETTING_UPFRONT_TWEALTH")]
    public class FEE_SETTING_UPFRONT_TWEALTH
    {
        public FEE_SETTING_UPFRONT_TWEALTH()
        {
            this.FSW_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }

        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [StringLength(36)]
        public string FSW_ID { get; set; }

        /// <summary>
        /// FK
        /// </summary>
        [StringLength(36)]
        public string FES_ID { get; set; }
        public virtual FEE_SETTING SettingOwner { get; set; }

        public decimal AGENT_RATE { get; set; }

        public decimal MKT_RATE { get; set; }

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
        public decimal RateAgentCalculated
        {
            get
            {
                return this.AGENT_RATE / 100m;
            }
        }

        [NotMapped]
        public decimal RateMktCalculated
        {
            get
            {
                return this.MKT_RATE / 100m;
            }
        }


    }
}
