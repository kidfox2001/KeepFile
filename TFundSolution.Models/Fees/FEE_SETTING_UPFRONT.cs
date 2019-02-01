using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models
{
    [Table("CIS.FEE_SETTING_UPFRONT")]
    public class FEE_SETTING_UPFRONT : IValidatableObject
    {

        public FEE_SETTING_UPFRONT()
        {
            this.FUF_ID = Guid.NewGuid().ToString();
            this.START_DATE = DateTime.Now;
            this.DataStatus = EnumDataStatus.NotChange;
        }

        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [StringLength(36)]
        public string FUF_ID { get; set; }

        /// <summary>
        /// FK
        /// </summary>
        [StringLength(36)]
        public string FES_ID { get; set; }
        public virtual FEE_SETTING SettingOwner { get; set; }

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

        #region Relation

        private ICollection<FEE_SETTING_UPFRONT_CHANNEL> _Channels = new HashSet<FEE_SETTING_UPFRONT_CHANNEL>();
        public virtual ICollection<FEE_SETTING_UPFRONT_CHANNEL> Channels
        {
            get { return _Channels; }
            set { _Channels = value; }
        }

        #endregion

        #region Method

        public FEE_SETTING_UPFRONT_CHANNEL NewChannel()
        {
            FEE_SETTING_UPFRONT_CHANNEL NewData = new FEE_SETTING_UPFRONT_CHANNEL();
            NewData.FUF_ID = this.FUF_ID;
            NewData.OfUpFront = this;
            NewData.DataStatus = EnumDataStatus.NewData;

            this.Channels.Add(NewData);

            return NewData;
        }

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
