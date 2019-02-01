using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TFundSolution.Models
{
    [Table("CIS.FEE_SETTING_ONLEVEL")]
    public class FEE_SETTING_ONLEVEL
    {
        public FEE_SETTING_ONLEVEL()
        {
            this.FSL_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }

        [Key]
        [StringLength(36)]
        public string FSL_ID { get; set; }

        [StringLength(36)]
        public string FSY_ID { get; set; }
        public virtual FEE_SETTING_YEAR OnYear { get; set; }

        public int LEVEL { get; set; }

        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [Range(0, 1000, ErrorMessage = "ค่าที่ระบุได้อยู่ระหว่าง 0-1000")]
        public decimal RATE { get; set; }

        [NotMapped]
        public decimal RateCalculated
        {
            get
            {
                return this.RATE / 100m;
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

    [Table("CIS.FEE_SETTING_ONLEVEL_REAL")]
    public class FEE_SETTING_ONLEVEL_REAL
    {
        public FEE_SETTING_ONLEVEL_REAL()
        {
            this.FSL_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }

        [Key]
        [StringLength(36)]
        public string FSL_ID { get; set; }

        [StringLength(36)]
        public string FSY_ID { get; set; }
        public virtual FEE_SETTING_YEAR OnYear { get; set; }

        public int LEVEL { get; set; }

        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [Range(0, 1000, ErrorMessage = "ค่าที่ระบุได้อยู่ระหว่าง 0-1000")]
        public decimal RATE { get; set; }

        [NotMapped]
        public decimal RateCalculated
        {
            get
            {
                return this.RATE / 100m;
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
