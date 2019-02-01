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

    [Table("CIS.FEE_ONGO_MKT")]
    public class FeeOngoMarketing
    {
        public FeeOngoMarketing()
        {
            this.FOM_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }


        [Key]
        [StringLength(36)]
        public string FOM_ID { get; set; }

        [ForeignKey("OfMarketingFee")]
        [StringLength(36)]
        public string FEM_ID { get; set; }
        public virtual FeeMarketing OfMarketingFee { get; set; }

        [Column(Order = 0)]
        [DataType(DataType.Date, ErrorMessage = "รูปแบบข้อมูลไม่ถูกต้อง")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime LOT_DATE_START { get; set; }

        [DataType(DataType.Date, ErrorMessage = "รูปแบบข้อมูลไม่ถูกต้อง")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime? LOT_DATE_END { get; set; }

        [NotMapped]
        public DateTime LotDateEndCalculated
        {
            get
            {
                return this.LOT_DATE_END ?? DateTime.Now.AddYears(2);
            }
        }

        public decimal UNIT_BY_LOT { get; set; }

        /// <summary>
        /// เป็นหน่วยที่หลังจากหักค่า bf แล้ว
        /// </summary>
        public decimal? UNIT_FOR_CAL { get; set; }

        public decimal FEE_BY_LOT { get; set; }

        /// <summary>
        /// settingOngo.RateAgentCalculated * settingOngo.RateMktCalculated
        /// </summary>
        public decimal? RATE_USED { get; set; }

        [NotMapped]
        public bool IsMatchRate
        {
            get
            {
                return (this.RATE_USED != null);
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

        /// <summary>
        /// คำนวนค่า fee ถ้ามีการพบการตั้งค่า จะมีการคำนวนร่วมกับค่า bf ด้วย ถ้ามีค่า bf
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public decimal CalculateFee()
        {
            var setting = this.OfMarketingFee.OnDateAgentFee.SettingOwner;

            if (setting != null)
            {
                var settingOngo = setting.GetOnGoSettingByCondition(this.LOT_DATE_START, this.OfMarketingFee.OnDateAgentFee.CalculateNetAmountAgent());
                if (settingOngo != null)
                {
                    this.RATE_USED = settingOngo.RateAgentCalculated * settingOngo.RateMktCalculated;
                    this.UNIT_FOR_CAL = this.UNIT_FOR_CAL ?? this.UNIT_BY_LOT; // ถ้ามีค่า unit cal แส่ดงว่าไม่โดนหักออกจาก bf ให้นำค่า unit lot มาใช้แทน
                    this.FEE_BY_LOT = ((((decimal)this.UNIT_FOR_CAL / this.OfMarketingFee.OnDateAgentFee.FUND_NET_SHARE) * this.OfMarketingFee.OnDateAgentFee.FUND_NET_AMOUNT * (this.OfMarketingFee.OnDateAgentFee.DiffFeeDate / 365m) * ((decimal)this.RATE_USED))).WithoutRounding(); 
                }
                else
                {
                    // ไม่เจอการตั้งค่า ไม่มีค่า fee 
                    this.FEE_BY_LOT = 0;
                }
            }

            return this.FEE_BY_LOT;

        }


        /// <summary>
        /// รับหน่วย bf มาลบจากหน่วยใน lot เผื่อคำนวนหน่วยที่จะต้องคำนวนจริงๆ จะคืนค่าหน่วย bf กลับไปหลังหักจากค่าหน่วยใน lot แล้ว
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public decimal RemoveUnitBf(decimal unit)
        {
            this.UNIT_FOR_CAL = this.UNIT_BY_LOT - unit < 0 ? 0 : this.UNIT_BY_LOT - unit;

            return unit - this.UNIT_BY_LOT;
        }

    }

}
