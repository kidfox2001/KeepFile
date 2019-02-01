using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using TFundSolution.Utils.EntityFramworks;

namespace TFundSolution.Models
{

    public interface IFeeSettingOngo
    {

    }

    [Table("CIS.FEE_SETTING_ONGO")]
    public class FEE_SETTING_ONGO : IValidatableObject
    {

        public FEE_SETTING_ONGO()
        {
            this.FOG_ID = Guid.NewGuid().ToString();
            this.START_DATE = DateTime.Now;
            this.DataStatus = EnumDataStatus.NotChange;
        }

        [Key]
        [StringLength(36)]
        public string FOG_ID { get; set; }

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
                return END_DATE !=null? ((DateTime)this.END_DATE).ToShortDateString() : "" ; 
            }
        }
        

        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        //[DisplayFormat( DataFormatString = "{0:0000}")]
        public decimal NET_AMOUNT { get; set; }

        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [Range(0, 1000, ErrorMessage = "ค่าที่ระบุได้อยู่ระหว่าง 0-1000")]
        public decimal AGENT_RATE { get; set; }

        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [Range(0, 1000, ErrorMessage = "ค่าที่ระบุได้อยู่ระหว่าง 0-1000")]
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

        #region Relation

        [StringLength(36)]
        public string FES_ID { get; set; }
        public FEE_SETTING SettingOwner { get; set; }

        #endregion

        #region Method


        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.START_DATE > this.END_DATE)
            {
                yield return new ValidationResult("วันที่เริ่ม ไม่สามารถมากกว่า สิ้นสุดวันที่", new[] { "START_DATE", "END_DATE" });
            }

        }


    }

    [Table("CIS.FEE_SETTING_ONGO_REAL")]
    public class FEE_SETTING_ONGO_REAL : IFeeSettingOngo, IValidatableObject
    {

        public FEE_SETTING_ONGO_REAL()
        {
            this.FOG_ID = Guid.NewGuid().ToString();
            this.START_DATE = DateTime.Now;
            this.DataStatus = EnumDataStatus.NotChange;
        }

        [Key]
        [StringLength(36)]
        public string FOG_ID { get; set; }

        [Column(Order = 0)]
        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [DataType(DataType.Date, ErrorMessage = "รูปแบบข้อมูลไม่ถูกต้อง")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime START_DATE { get; set; }

        [DataType(DataType.Date, ErrorMessage = "รูปแบบข้อมูลไม่ถูกต้อง")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime? END_DATE { get; set; }


        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        //[DisplayFormat( DataFormatString = "{0:0000}")]
        public decimal NET_AMOUNT { get; set; }

        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [Range(0, 1000, ErrorMessage = "ค่าที่ระบุได้อยู่ระหว่าง 0-1000")]
        public decimal AGENT_RATE { get; set; }

        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [Range(0, 1000, ErrorMessage = "ค่าที่ระบุได้อยู่ระหว่าง 0-1000")]
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

        #region Relation

        [StringLength(36)]
        public string FES_ID { get; set; }
        public FEE_SETTING SettingOwner { get; set; }

        #endregion

        #region Method


        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.START_DATE > this.END_DATE)
            {
                yield return new ValidationResult("วันที่เริ่ม ไม่สามารถมากกว่า สิ้นสุดวันที่", new[] { "START_DATE", "END_DATE" });
            }

        }

    }
}
