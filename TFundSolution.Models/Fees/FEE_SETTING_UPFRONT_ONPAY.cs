using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TFundSolution.Models
{

    [Table("CIS.FEE_SETTING_UPFRONT_ONPAY")]
    public class FEE_SETTING_UPFRONT_ONPAY
    {

        public FEE_SETTING_UPFRONT_ONPAY()
        {
            this.FFP_ID = Guid.NewGuid().ToString();
            this.START_DATE = DateTime.Now;
            this.DataStatus = EnumDataStatus.NotChange;
        }


        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [StringLength(36)]
        public string FFP_ID { get; set; }

        /// <summary>
        /// FK
        /// </summary>
        [StringLength(36)]
        public string FES_ID { get; set; }
        public virtual FEE_SETTING_TBANK SettingOwner { get; set; }


        [Column(Order = 0)]
        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [DataType(DataType.Date, ErrorMessage = "รูปแบบข้อมูลไม่ถูกต้อง")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime START_DATE { get; set; }

        [DataType(DataType.Date, ErrorMessage = "รูปแบบข้อมูลไม่ถูกต้อง")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime? END_DATE { get; set; }

        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [Range(0, 1000, ErrorMessage = "ค่าที่ระบุได้อยู่ระหว่าง 0-1000")]
        public decimal AGENT_RATE { get; set; }

        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [Range(0, 1000, ErrorMessage = "ค่าที่ระบุได้อยู่ระหว่าง 0-1000")]
        public decimal MKT_RATE { get; set; }

        [NotMapped]
        public DateTime DateEndCalculated
        {
            get
            {
                return this.END_DATE ?? DateTime.Now.AddYears(2);
            }
        }

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
