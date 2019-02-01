using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models
{

    [Table("CIS.FEE_SETTING_ONPAY")]
    public class FEE_SETTING_ONPAY : IValidatableObject
    {

        public FEE_SETTING_ONPAY()
        {
            this.FOP_ID = Guid.NewGuid().ToString();
            this.START_DATE = DateTime.Now;
            this.DataStatus = EnumDataStatus.NotChange;
        }

        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [StringLength(36)]
        public string FOP_ID { get; set; }

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

        [NotMapped]
        public string DisplayEndDate
        {
            get
            {
                return END_DATE != null ? ((DateTime)this.END_DATE).ToShortDateString() : "";
            }
        }

        [NotMapped]
        public DateTime DateEndCalculated
        {
            get
            {
                return this.END_DATE ?? DateTime.Now.AddYears(2);
            }
        }

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


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.START_DATE > this.END_DATE)
            {
                yield return new ValidationResult("วันที่เริ่ม ไม่สามารถมากกว่า สิ้นสุดวันที่", new[] { "START_DATE", "END_DATE" });
            }

        }
    }

    [Table("CIS.FEE_SETTING_ONPAY_REAL")]
    public class FEE_SETTING_ONPAY_REAL : IValidatableObject
    {

        public FEE_SETTING_ONPAY_REAL()
        {
            this.FOP_ID = Guid.NewGuid().ToString();
            this.START_DATE = DateTime.Now;
            this.DataStatus = EnumDataStatus.NotChange;
        }

        [Key]
        [StringLength(36)]
        public string FOP_ID { get; set; }

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

        [NotMapped]
        public string DisplayEndDate
        {
            get
            {
                return END_DATE != null ? ((DateTime)this.END_DATE).ToShortDateString() : "";
            }
        }

        [NotMapped]
        public DateTime DateEndCalculated
        {
            get
            {
                return this.END_DATE ?? DateTime.Now.AddYears(2);
            }
        }

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


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.START_DATE > this.END_DATE)
            {
                yield return new ValidationResult("วันที่เริ่ม ไม่สามารถมากกว่า สิ้นสุดวันที่", new[] { "START_DATE", "END_DATE" });
            }

        }
    }

}
