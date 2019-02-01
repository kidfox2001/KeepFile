using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TFundSolution.Models
{

    [Table("CIS.FEE_SETTING_UPFRONT_ONLEVEL")]
    public class FEE_SETTING_UPFRONT_ONLEVEL
    {
        public FEE_SETTING_UPFRONT_ONLEVEL()
        {
            this.FUL_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }

        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [StringLength(36)]
        public string FUL_ID { get; set; }

        /// <summary>
        /// FK
        /// </summary>
        [StringLength(36)]
        public string FES_ID { get; set; }
        public virtual FEE_SETTING_TBANK SettingOwner { get; set; }

        public int LEVEL { get; set; }

        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [Range(0, 1000, ErrorMessage = "ค่าที่ระบุได้อยู่ระหว่าง 0-1000")]
        public decimal RATE { get; set; }

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
        public bool IsShow
        {
            get
            {
                return this.DataStatus != EnumDataStatus.DeleteData;
            }
        }

        [NotMapped]
        public decimal RateCalculated
        {
            get
            {
                return this.RATE / 100m;
            }
        }

    }

    

}
