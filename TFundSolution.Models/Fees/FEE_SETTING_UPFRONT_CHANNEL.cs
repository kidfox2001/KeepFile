using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models
{

    [Table("CIS.FEE_SETTING_UPFRONT_CHANNEL")]
    public class FEE_SETTING_UPFRONT_CHANNEL
    {

        public FEE_SETTING_UPFRONT_CHANNEL()
        {
            this.FUC_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }


        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [StringLength(36)]
        public string FUC_ID { get; set; }

        /// <summary>
        /// FK
        /// </summary>
        [StringLength(36)]
        public string FUF_ID { get; set; }
        public virtual FEE_SETTING_UPFRONT OfUpFront { get; set; }

        [StringLength(15)]
        public string CHANNEL { get; set; }

        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [Range(0, 1000, ErrorMessage = "ค่าที่ระบุได้อยู่ระหว่าง 0-1000")]
        public decimal AGENT_RATE { get; set; }


        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [Range(0, 1000, ErrorMessage = "ค่าที่ระบุได้อยู่ระหว่าง 0-1000")]
        public decimal MKT_RATE { get; set; }

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
                return this.AGENT_RATE * this.MKT_RATE / 100m;
            }
        }

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

    }
}
