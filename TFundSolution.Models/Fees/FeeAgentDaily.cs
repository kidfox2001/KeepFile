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
    [Table("CIS.FEE_AGENT_DAILY")]
    public class FeeAgentDaily
    {

        public FeeAgentDaily()
        {
            this.FAD_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }

        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [StringLength(36)]
        public string FAD_ID { get; set; }

        [ForeignKey("FundOwner")]
        [StringLength(30)]
        [Column(Order = 1)]
        public string FUND_ID { get; set; }
        public virtual Fund FundOwner { get; set; }

        [ForeignKey("AgentOwner")]
        [StringLength(9)]
        [Column(Order = 2)]
        public string AGENT_ID { get; set; }
        public virtual Agent AgentOwner { get; set; }

        [ForeignKey("SettingOwner")]
        [StringLength(36)]
        public string FES_ID { get; set; }
        public virtual FEE_SETTING SettingOwner { get; set; }

        [Column(Order = 0)]
        [DataType(DataType.Date, ErrorMessage = "รูปแบบข้อมูลไม่ถูกต้อง")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime FEE_DATE { get; set; }

        [DataType(DataType.Date, ErrorMessage = "รูปแบบข้อมูลไม่ถูกต้อง")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime FEE_BEFORE_DATE { get; set; }

        /// <summary>
        /// วันที่ ที่มี nav ก่อนจะเป็นวันหยุด (วันที่นี้จะเกิดเมื่อกองมีวันหยุด ที่ไม่ตรงกับกองส่วนใหญ่ เอาไว้ใช็คำนวน div date เมื่อกองนี้กลับมามี nav)
        /// </summary>
        [DataType(DataType.Date, ErrorMessage = "รูปแบบข้อมูลไม่ถูกต้อง")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime? HOLIDAY_BEFORE_DATE { get; set; }

        [NotMapped]
        public Int16 DiffFeeDate
        {
            get
            {
                return Convert.ToInt16((this.FEE_DATE - this.FEE_BEFORE_DATE).TotalDays);
            }
        }

        [NotMapped]
        public int DiffFeeYear
        {
            get
            {
                return (this.FEE_DATE.Year - this.FEE_BEFORE_DATE.Year);
            }
        }

        /// <summary>
        /// มูลค่าทั้งหมดของกอง
        /// </summary>
        public decimal FUND_NET_AMOUNT { get; set; }

        /// <summary>
        /// จำนวนหุ้นทั้งหมดของกอง
        /// </summary>
        public decimal FUND_NET_SHARE { get; set; }

        public decimal UNIT_BUY { get; set; }

        public decimal UNIT_SELL { get; set; }

        public decimal UNIT_TRANIN { get; set; }

        public decimal UNIT_TRANOUT { get; set; }

        /// <summary>
        /// จำนวนหน่วยทีควรนำเข้าทั้งหมด
        /// </summary>
        public decimal UNIT_ADD { get; set; }

        /// <summary>
        /// จำนวนหน่วยทีควรนำออกทั้งหมด
        /// </summary>
        public decimal UNIT_OUT { get; set; }

        /// <summary>
        /// จำนวนหน่วยที่นำเข้า จริงจากการคำนวน ongo (<= UNIT_ADD)
        /// </summary>
        public decimal UNIT_ADD_ONGO { get; set; }

        /// <summary>
        /// จำนวนหน่วยที่นำออก จริงจากการคำนวน ongo  (<= UNIT_OUT)
        /// </summary>
        public decimal UNIT_OUT_ONGO { get; set; }

        public decimal UNIT_BF { get; set; }

        public decimal? TOTAL_UNIT_ONGO { get; set; }

        public decimal? TOTAL_FEE_ONGO { get; set; }

        public decimal? TOTAL_FEE_UPFRONT { get; set; }

        public decimal? TOTAL_FEE_UPFRONT_NOTPAID { get; set; }

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

        //[NotMapped]
        //public FEE_SETTING GetSettingPassAgent
        //{
        //    get
        //    {
        //        return this.AgentOwner.GetSettingByFund(this.FUND_ID);
        //    }
        //}

        #region Relation

        private ICollection<FeeOngoAgent> _OngoDetails = new HashSet<FeeOngoAgent>();
        public virtual ICollection<FeeOngoAgent> OngoDetails
        {
            get { return _OngoDetails; }
            set { _OngoDetails = value; }
        }

        private ICollection<FeeUpFrontAgent> _UpFrontDetails = new HashSet<FeeUpFrontAgent>();
        public virtual ICollection<FeeUpFrontAgent> UpFrontDetails
        {
            get { return _UpFrontDetails; }
            set { _UpFrontDetails = value; }
        }

        private ICollection<FeeMarketing> _MarketingFees = new HashSet<FeeMarketing>();
        public virtual ICollection<FeeMarketing> MarketingFees
        {
            get { return _MarketingFees; }
            set { _MarketingFees = value; }
        }

        #endregion

        #region Method

        /// <summary>
        /// เรียกข้อมูล marketing โดย code
        /// </summary>
        /// <param name="mktCode"></param>
        /// <returns></returns>
        public FeeMarketing GetMarktingByCode(string mktCode)
        {
            return this.MarketingFees.SingleOrDefault(m => m.MKT == mktCode);
        }

        /// <summary>
        /// คัดลอก Fee Ongo ทั้งหมด (LOT_DATE_START,LOT_DATE_END,UNIT_BY_LOT)
        /// </summary>
        /// <param name="feeDate"></param>
        /// <returns></returns>
        public List<FeeOngoAgent> DuplicateOngoDetails()
        {

            List<FeeOngoAgent> ongos = new List<FeeOngoAgent>();

            foreach (var itemDetail in this.OngoDetails)
            {
                FeeOngoAgent newOngo = new FeeOngoAgent()
                {
                    LOT_DATE_START = itemDetail.LOT_DATE_START,
                    LOT_DATE_END = itemDetail.LOT_DATE_END,
                    UNIT_BY_LOT = itemDetail.UNIT_BY_LOT,
                    FEE_BY_LOT = 0
                };

                ongos.Add(newOngo);
            }

            return ongos;
        }

        /// <summary>
        /// เพิ่ม Fee Ongo Detail ที่ส่งเข้ามา
        /// </summary>
        /// <param name="ongos"></param>
        public void TranferOngoDetails(IEnumerable<FeeOngoAgent> ongos)
        {
            foreach (var itemOngo in ongos)
            {
                var newOngo = this.NewOngoDetail();
                newOngo.LOT_DATE_START = itemOngo.LOT_DATE_START;
                newOngo.LOT_DATE_END = itemOngo.LOT_DATE_END;
                newOngo.UNIT_BY_LOT = itemOngo.UNIT_BY_LOT;
                newOngo.FEE_BY_LOT = 0;
            }
        }


        /// <summary>
        /// ปรับ lot ongo ใหม่เทียบกับที่ตั้งค่าไว้ จะเพิ่มเฉพาะ lotdate ที่ตั้งค่ามีค่ามากกว่า lotdate ล่าสุดที่มีอยู่แล้ว
        ///  ลบ Lot ที่ไม่มีอยู่ใน setting ทิ้ง
        /// </summary>
        /// <param name="lotDate"></param>
        /// <returns></returns>
        public void ReLotDateFromSetting()
        {
            if (this.SettingOwner != null)
            {
                var settingOngoInRange = this.SettingOwner.GetOngoLotDateLessThanByDate(this.FEE_DATE);

                if (settingOngoInRange.Any())
                {
                    // ลบ Lot ที่ไม่มีอยู่ใน setting ทิ้ง
                    foreach (var itemOngo in this.OngoDetails)
                    {
                        if (!settingOngoInRange.Any(m =>
                            m.START_DATE == itemOngo.LOT_DATE_START &
                            m.END_DATE == itemOngo.LOT_DATE_END))
                        {
                            this.OngoDetails.Remove(itemOngo);
                        }
                    }

                    // จะสนใจแต่ lot ตั้งค่าที่มีค่ามากกว่า lot date ที่ล่าสุดของที่มีอยู่ ถึงจะนำเอา
                    DateTime lotdateMax = new DateTime(2010, 1, 1);
                    if (this.OngoDetails.Any())
                    {
                        lotdateMax = this.OngoDetails.Max(m => m.LOT_DATE_START);
                    }

                    var lotAdds = settingOngoInRange.Where(m => m.START_DATE > lotdateMax);
                    foreach (var itemSettings in lotAdds)
                    {
                        FeeOngoAgent newOngo = this.NewOngoDetail();
                        newOngo.LOT_DATE_START = itemSettings.START_DATE;
                        newOngo.LOT_DATE_END = itemSettings.END_DATE;
                    }
                }
            }

        }

        public FeeOngoAgent NewOngoDetail()
        {
            FeeOngoAgent newOngo = new FeeOngoAgent()
            {
                FAD_ID = this.FAD_ID,
                OnDateAgentFee = this,
                FEE_BY_LOT = 0,
                UNIT_BY_LOT = 0
            };

            this.OngoDetails.Add(newOngo);

            return newOngo;
        }

        public FeeUpFrontAgent NewUpFrontDetail(string channel, string isTWealth, string isTWealthPaid, decimal totalMoney)
        {
            FeeUpFrontAgent newUpfront = new FeeUpFrontAgent()
            {
                FAD_ID = this.FAD_ID,
                OnDateAgentFee = this,
                CHANNEL = channel,
                TWEALTH = isTWealth,
                TWEALTH_PAY = isTWealthPaid,
                TOTAL_MONEY = totalMoney
            };

            this.UpFrontDetails.Add(newUpfront);

            return newUpfront;
        }

        /// <summary>
        /// เพิ่มใหม่ marketing 
        /// </summary>
        /// <param name="mktCode"></param>
        /// <returns></returns>
        public FeeMarketing NewFeeMarketing(string mkt)
        {
            FeeMarketing newMkt = new FeeMarketing()
            {
                FAD_ID = this.FAD_ID,
                FUND_ID = this.FUND_ID,
                AGENT_ID = this.AGENT_ID,
                AGENT = this.AGENT_ID.Substring(0, 3),
                BRANCH = this.AGENT_ID.Substring(3, 6),
                MKT = mkt,
                FEE_DATE = this.FEE_DATE,
                FES_ID = this.FES_ID,

                OnDateAgentFee = this
            };

            this.MarketingFees.Add(newMkt);

            return newMkt;
        }

        public FeeMarketing NewFeeMarketingByPrevious(FeeMarketing mktPrevious)
        {
            var newMkt = this.NewFeeMarketing(mktPrevious.MKT);
            newMkt.UNIT_BF = mktPrevious.UNIT_BF;
            newMkt.UNIT_BF_BEFORE_DATE = mktPrevious.UNIT_BF;

            return newMkt;
        }

        /// <summary>
        /// เพิ่ม marketing ใหม่ถ้ายังไม่มี
        /// </summary>
        /// <param name="mkt"></param>
        /// <returns></returns>
        public FeeMarketing AddjustFeeMarketing(string mkt)
        {
            var newMkt = this.MarketingFees.SingleOrDefault(m => m.MKT == mkt);
            if (newMkt == null)
            {
                newMkt = this.NewFeeMarketing(mkt);
            }

            return newMkt;
        }

        /// <summary>
        /// เพิ่ม marketing ใหม่ถ้ายังไม่มี
        /// </summary>
        /// <param name="mkt"></param>
        /// <returns></returns>
        public FeeMarketing AddjustFeeMarketingByPrevious(FeeMarketing mktPrevious)
        {
            var newMkt = this.MarketingFees.SingleOrDefault(m => m.MKT == mktPrevious.MKT);
            if (newMkt == null)
            {
                newMkt = this.NewFeeMarketingByPrevious(mktPrevious);
            }
            return newMkt;

        }
     
        /// <summary>
        /// เพิ่มหน่วยให้ lot ล่าสุด และ ต้องอยู่ในขอบเขต lot date ด้วย ไม่งั้นจะไม่เพิ่มหน่วยลงไป
        /// ลบหน่วยให้ lot หลังสุด ลบจนกว่าจะหมด unit ที่ส่งมาให้ลบ และถ้า lotdate ไหนหมดทำการลบ lot date detail นั้นทิ้งเลย
        /// </summary>
        /// <param name="unitBuy"></param>
        /// <param name="unitSell"></param>
        /// <param name="unitTranIn"></param>
        /// <param name="unitTranOut"></param>
        /// <param name="navDate"></param>
        public void ReUnitOngo(decimal unitBuy, decimal unitSell, decimal unitTranIn, decimal unitTranOut, DateTime navDate)
        {
            this.UNIT_BUY = unitBuy;
            this.UNIT_SELL = unitSell;
            this.UNIT_TRANIN = unitTranIn;
            this.UNIT_TRANOUT = unitTranOut;

            // เพิ่ม
            var unitAdd = unitBuy + unitTranIn;
            this.UNIT_ADD = unitAdd;
            this.UNIT_ADD_ONGO = 0;

            var feeOngo = this.OngoDetails.OrderByDescending(m => m.LOT_DATE_START).FirstOrDefault();
            if (feeOngo != null)
            {
                // หา lot ล่าสุดมาใส่
                if (feeOngo.LOT_DATE_START <= navDate & navDate <= feeOngo.LotDateEndCalculated)
                {
                    // หน่วยที่จะเอามาลงต้องอยู่ในช่วงเวลาของ lot นั้นด้วย
                    this.UNIT_ADD_ONGO = unitAdd;
                    feeOngo.UNIT_BY_LOT += unitAdd;
                }
            }

            // ลด
            var unitRemove = unitSell + unitTranOut;
            this.UNIT_OUT = unitRemove;
            this.UNIT_OUT_ONGO = 0;

            while (unitRemove > 0)
            {
                feeOngo = this.OngoDetails.OrderBy(m => m.LOT_DATE_START).FirstOrDefault();
                if (feeOngo != null)
                {
                    if (unitRemove > feeOngo.UNIT_BY_LOT)
                    {
                        // แบบเหลือ unit ที่ส่งเข้า
                        this.UNIT_OUT_ONGO += unitRemove - feeOngo.UNIT_BY_LOT;
                        unitRemove -= feeOngo.UNIT_BY_LOT;
                        this.OngoDetails.Remove(feeOngo);
                    }
                    else
                    {
                        // แบบ unit ที่ส่งเข้ามาหมด
                        this.UNIT_OUT_ONGO += unitRemove;
                        feeOngo.UNIT_BY_LOT -= unitRemove;
                        unitRemove = 0;
                    }
                }
                else
                {
                    // ถ้าไม่เจอ lot ให้ออกไปเลย
                    unitRemove = 0;
                }
            }

            this.SumUnitOngo();
            this.ReUnitOngoWithBfValue();
        }

        public decimal SumUnitOngo()
        {
            this.TOTAL_UNIT_ONGO = this.OngoDetails.Sum(m => m.UNIT_BY_LOT);
            return (decimal)this.TOTAL_UNIT_ONGO;
        }

        /// <summary>
        /// คำนวนค่า fee ongo ทั้งวัน
        /// </summary>
        /// <returns></returns>
        public decimal SumFeeOngo()
        {
            return this.OngoDetails.Sum(m => m.FEE_BY_LOT);
        }

        public decimal SumFeeMarketingOngo()
        {
            return this.MarketingFees.Sum(m => (decimal)m.TOTAL_FEE_ONGO);
        }

        /// <summary>
        /// คำนวนมูลค่าหุุ้นทั้ง agent หน่วยหลัก พันล้านบาท
        /// </summary>
        /// <returns></returns>
        public decimal CalculateNetAmountAgent()
        {
            decimal result = (((decimal)this.TOTAL_UNIT_ONGO / this.FUND_NET_SHARE) * this.FUND_NET_AMOUNT) / 1000000;

            return result;
        }

        public decimal CalculateFeeOngo()
        {
            this.TOTAL_FEE_ONGO = this.OngoDetails.Sum(m => m.CalculateFee());
            return (decimal)this.TOTAL_FEE_ONGO;
        }

        public decimal CalculateFeeUpFront()
        {
            this.UpFrontDetails.Sum(m => m.CalculateFee());
            this.TOTAL_FEE_UPFRONT = this.UpFrontDetails.Where(m => m.IS_PAID).Sum(m => m.FEE);
            this.TOTAL_FEE_UPFRONT_NOTPAID = this.UpFrontDetails.Where(m => !m.IS_PAID).Sum(m => m.FEE);

            return (decimal)this.TOTAL_FEE_UPFRONT;
        }

        public decimal CalculateNav()
        {
            return this.FUND_NET_AMOUNT / this.FUND_NET_SHARE;
        }

        /// <summary>
        /// ปรับจำนวนหน่วยใน lot ใหม่โดยหักจาก bf ออกไปถ้ามีค่า bf
        /// </summary>
        public void ReUnitOngoWithBfValue()
        {
            if (this.SettingOwner.AgentOwner != null)
            {
                this.UNIT_BF = this.SettingOwner.AgentOwner.GetAgentBfValueByFund(this.FUND_ID);

                if (this.UNIT_BF > 0)
                {
                    var unitBf = this.UNIT_BF;
                    foreach (var itemOngo in this.OngoDetails.OrderBy(m => m.LOT_DATE_START))
                    {
                        unitBf = itemOngo.RemoveUnitBf(unitBf);
                        if (unitBf <= 0)
                        {
                            // ถ้าค่า bf หมดก็ไม่ต้องหักออกต่อ
                            break;
                        }
                    }
                }
            }


        }

        #endregion

    }
}
