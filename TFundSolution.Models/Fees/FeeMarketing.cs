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

    [Table("CIS.FEE_MKT")]
    public class FeeMarketing
    {

        public FeeMarketing()
        {
            this.FEM_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }

        [Key]
        [StringLength(36)]
        public string FEM_ID { get; set; }

        [ForeignKey("OnDateAgentFee")]
        [StringLength(36)]
        public string FAD_ID { get; set; }
        public virtual FeeAgentDaily OnDateAgentFee { get; set; }

        [ForeignKey("OfMarketing")]
        [Column(Order = 1)]
        public string AGENT { get; set; }

        [ForeignKey("OfMarketing")]
        [Column(Order = 2)]
        public string BRANCH { get; set; }

        [ForeignKey("OfMarketing")]
        [Column(Order = 3)]
        [Required(ErrorMessage = "ข้อมูลนี้ต้องระบุ")]
        [StringLength(4)]
        public string MKT { get; set; }
        public virtual Marketing OfMarketing { get; set; }

        public DateTime FEE_DATE { get; set; }

        [ForeignKey("SettingOwner")]
        public string FES_ID { get; set; }
        public virtual FEE_SETTING SettingOwner { get; set; }

        public string FUND_ID { get; set; }

        public string AGENT_ID { get; set; }

        public decimal UNIT_BUY { get; set; }

        public decimal UNIT_SELL { get; set; }

        public decimal UNIT_TRANIN { get; set; }

        public decimal UNIT_TRANOUT { get; set; }

        public decimal UNIT_ADD { get; set; }

        public decimal UNIT_OUT { get; set; }

        public decimal UNIT_ADD_ONGO { get; set; }

        public decimal UNIT_OUT_ONGO { get; set; }

        public decimal UNIT_ADD_ONYEAR { get; set; }

        public decimal UNIT_OUT_ONYEAR { get; set; }

        public decimal? TOTAL_UNIT_ONGO { get; set; }

        /// <summary>
        /// unit ongo ของวันก่อนหน้าถ้าไม่มีเท่ากับ 0
        /// </summary>
        public decimal TOTAL_UNIT_ONGO_BEFORE_DATE { get; set; }

        public decimal? RATE_ONGO_ONPAY_USED { get; set; }

        public decimal? RATE_UPFRONT_ONPAY_USED { get; set; }

        public decimal? RATE_UPFRONT_TWEALTH_USED { get; set; }

        public decimal? TOTAL_FEE_ONGO_ONPAY { get; set; }

        public decimal? TOTAL_FEE_ONGO { get; set; }

        public decimal? TOTAL_FEE_ONGO_ONYEAR { get; set; }

        public decimal? TOTAL_FEE_ONGO_REWARD { get; set; }

        public decimal? TOTAL_FEE_UPFRONT { get; set; }

        public decimal? TOTAL_FEE_UPFRONT_NOTPAID { get; set; }

        public decimal? TOTAL_FEE_UPFRONT_ONPAY { get; set; }

        public decimal? TOTAL_FEE_UPFRONT_REWARD { get; set; }

        /// <summary>
        /// เก็บ unit bfrmf เก่า จะมีแต่กลุ่ม IsExtraFund ที่มีค่านี้
        /// </summary>
        public decimal UNIT_BF { get; set; }

        /// <summary>
        /// เก็บ unit bfrmf เก่า จะมีแต่กลุ่ม IsExtraFund ที่มีค่านี้ของวันก่อนหน้า
        /// </summary>
        public decimal UNIT_BF_BEFORE_DATE { get; set; }

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

        //[InverseProperty("OfMarketingOngo")]
        private ICollection<FeeOngoMarketing> _OngoDetails = new HashSet<FeeOngoMarketing>();
        public virtual ICollection<FeeOngoMarketing> OngoDetails
        {
            get { return _OngoDetails; }
            set { _OngoDetails = value; }
        }

        //[InverseProperty("OfMarketingYear")]
        private ICollection<FeeOngoOnYear> _OnYears = new HashSet<FeeOngoOnYear>();
        public virtual ICollection<FeeOngoOnYear> OnYears
        {
            get { return _OnYears; }
            set { _OnYears = value; }
        }

        private ICollection<FeeUpFrontMarketing> _UpFrontDetails = new HashSet<FeeUpFrontMarketing>();
        public virtual ICollection<FeeUpFrontMarketing> UpFrontDetails
        {
            get { return _UpFrontDetails; }
            set { _UpFrontDetails = value; }
        }

        #endregion

        #region Method

        public List<FeeOnGoReward> GetAllFeeGoReward()
        {
            List<FeeOnGoReward> rewards = new List<FeeOnGoReward>();
            foreach (var itemYear in this.OnYears)
            {
                rewards.AddRange(itemYear.OnLevelDetails);
            }
            return rewards;
        }

        public List<FeeUpFrontReward> GetAllFeeUpFrontReward()
        {
            List<FeeUpFrontReward> rewards = new List<FeeUpFrontReward>();

            foreach (var itemUpFront in this.UpFrontDetails)
            {
                rewards.AddRange(itemUpFront.OnLevelDetails);
            }

            return rewards;
        }

        public decimal GetUnitOnZeroYear()
        {
            var findOnYear = this.OnYears.SingleOrDefault(m => m.IsYearZero) ?? new FeeOngoOnYear();
            var unit = findOnYear.UNIT_BY_YEAR;

            return unit;
        }

        public FeeOngoOnYear GetYearDetailByYear(int onYear)
        {
            var findOnYear = this.OnYears.SingleOrDefault(m => m.YEAR_ORDER == onYear.ToString()) ?? new FeeOngoOnYear();

            return findOnYear;
        }

        public FeeOngoMarketing NewOngoDetail()
        {
            FeeOngoMarketing newItem = new FeeOngoMarketing()
            {
                FEM_ID = this.FEM_ID,
                OfMarketingFee = this,
                FEE_BY_LOT = 0,
                UNIT_BY_LOT = 0
            };

            this.OngoDetails.Add(newItem);

            return newItem;
        }

        public FeeUpFrontMarketing NewUpFrontDetail(string channel, string isTWealth, string isTWealthPaid, decimal totalMoney)
        {
            FeeUpFrontMarketing newUpfront = new FeeUpFrontMarketing()
            {
                FEM_ID = this.FEM_ID,
                OfMarketingFee = this,
                CHANNEL = channel,
                TWEALTH = isTWealth,
                TWEALTH_PAY = isTWealthPaid,
                TOTAL_MONEY = totalMoney
            };

            this.UpFrontDetails.Add(newUpfront);

            return newUpfront;
        }

        public FeeOngoOnYear NewOnYearDetail()
        {
            FeeOngoOnYear newItem = new FeeOngoOnYear()
            {
                FEM_ID = this.FEM_ID,
                OfMarketingFee = this,
                UNIT_BY_YEAR = 0
            };

            this.OnYears.Add(newItem);

            return newItem;
        }

        public List<FeeOngoMarketing> DuplicateOngoDetails()
        {
            List<FeeOngoMarketing> ongos = new List<FeeOngoMarketing>();

            foreach (var itemDetail in this.OngoDetails)
            {
                FeeOngoMarketing newOngo = new FeeOngoMarketing()
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

        public List<FeeOngoOnYear> DuplicateOnYearDetails()
        {
            List<FeeOngoOnYear> onyears = new List<FeeOngoOnYear>();

            foreach (var itemDetail in this.OnYears)
            {
                FeeOngoOnYear newItem = new FeeOngoOnYear()
                {
                    UNIT_BY_YEAR = itemDetail.UNIT_BY_YEAR,
                    YEAR_ORDER = itemDetail.YEAR_ORDER
                };

                onyears.Add(newItem);
            }

            return onyears;
        }

        /// <summary>
        /// รวมยอดค่า fee ong ทั้งหมด
        /// </summary>
        /// <returns></returns>
        public decimal SumFeeOngo()
        {
            this.TOTAL_FEE_ONGO = this.OngoDetails.Sum(m => m.FEE_BY_LOT);

            return (decimal)this.TOTAL_FEE_ONGO;
        }

        /// <summary>
        /// รวมยอด หน่วยลงทุน ongo ทั้งหมด
        /// </summary>
        /// <returns></returns>
        public decimal SumUnitOngo()
        {
            this.TOTAL_UNIT_ONGO = this.OngoDetails.Sum(m => m.UNIT_BY_LOT);

            return (decimal)this.TOTAL_UNIT_ONGO;
        }

        /// <summary>
        /// รับการถ่ายโอน ongo detail เข้ามา ใช้สำหรับการโอนหน่วยจากวันก่อนหน้า รวมยอดหน่วยทุก lot เก็บเข้า column TOTAL_UNIT_ONGO_BF ด้วย
        /// </summary>
        /// <param name="ongos"></param>
        public void TranferOngoDetails(IEnumerable<FeeOngoMarketing> ongos)
        {

            foreach (var itemOngo in ongos)
            {
                var newOngo = this.NewOngoDetail();
                newOngo.LOT_DATE_START = itemOngo.LOT_DATE_START;
                newOngo.LOT_DATE_END = itemOngo.LOT_DATE_END;
                newOngo.UNIT_BY_LOT = itemOngo.UNIT_BY_LOT;
                newOngo.FEE_BY_LOT = 0;
            }

            this.TOTAL_UNIT_ONGO_BEFORE_DATE = this.OngoDetails.Sum(m => m.UNIT_BY_LOT);
        }

        /// <summary>
        /// รับการถ่ายโอน year detail เข้ามา ใช้สำหรัการโอนหน่วยจากวันก่อนหน้า
        /// </summary>
        public void TranferYearDetails(IEnumerable<FeeOngoOnYear> years)
        {
            foreach (var itemYear in years)
            {
                var newYear = this.NewOnYearDetail();
                newYear.UNIT_BY_YEAR = itemYear.UNIT_BY_YEAR;
                newYear.YEAR_ORDER = itemYear.YEAR_ORDER;
            }
        }

        /// <summary>
        /// ทำการปรับ lot ตามที่ตั้งค่าไว้ ถ้าเป็นการคำนวนข้ามปีจะทำการ ship unit ให้ด้วย
        /// </summary>
        /// <param name="setting"></param>
        public void ReLotYearFormSetting()
        {
            if (this.OnDateAgentFee.SettingOwner != null)
            {
                // ควรมี lot เท่ากับ lot ที่มีการตั้งค่าไว้ 
                var settingYears = ((FEE_SETTING_TBANK)this.OnDateAgentFee.SettingOwner).SettingOngoYears;

                if (settingYears.Any())
                {
                    // เพิ่ม lot
                    var yearSettings = settingYears.Select(m => m.YearForResult).ToList();
                    var yearResults = this.OnYears.Select(m => m.YEAR_ORDER).ToList();

                    // เอาเฉพาะปีที่มีการตั้งค่า แล้วไม่มีใน lot มาเพิ่ม lot เข้าไป
                    var addYears = yearSettings.Where(q => !yearResults.Contains(q)).ToList();
                    foreach (var year in addYears)
                    {
                        var newLotYear = NewOnYearDetail();
                        newLotYear.YEAR_ORDER = year;
                    }

                    // ลด lot ในกรณีที่มีการตั้งค่า แบบลดลงต้องเอา lot นั้นออกไปไม่งั้นจะมีการคิดค่า fee
                    var removeYears = yearResults.Where(q => !yearSettings.Contains(q)).ToList();
                    foreach (var year in removeYears)
                    {
                        FeeOngoOnYear itemYear = this.OnYears.Single(q => q.YEAR_ORDER == year);
                        this.OnYears.Remove(itemYear);
                    }
                }


                // ถ้าขึ้นปีใหม่ ทำการ ship unit ไปปีถัดไป
                if (this.OnYears.Any() & this.OnDateAgentFee.DiffFeeYear == 1)
                {

                    decimal unitTranfer = 0;
                    foreach (var itemYear in this.OnYears.OrderBy(m => m.YEAR_ORDER))
                    {
                        decimal temp = 0;
                        if (itemYear.YEAR_ORDER == "1")
                        {
                            unitTranfer = itemYear.UNIT_BY_YEAR;
                            itemYear.UNIT_BY_YEAR = 0;
                        }
                        else
                        {
                            temp = itemYear.UNIT_BY_YEAR;
                            itemYear.UNIT_BY_YEAR = unitTranfer;
                            unitTranfer = temp;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// ปรับ lot ongo ใหม่เทียบกับที่ตั้งค่าไว้ จะเพิ่มเฉพาะ lotdate ที่ตั้งค่ามีค่ามากกว่า lotdate ล่าสุดที่มีอยู่แล้ว
        /// ลบ Lot ที่ไม่มีอยู่ใน setting ทิ้ง
        /// </summary>
        /// <param name="lotDate"></param>
        /// <returns></returns>
        public void ReLotDateFromSetting()
        {
            if (this.OnDateAgentFee.SettingOwner != null)
            {
                var settingOngoInRange = this.OnDateAgentFee.SettingOwner.GetOngoLotDateLessThanByDate(this.OnDateAgentFee.FEE_DATE);

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
                        var newOngo = this.NewOngoDetail();
                        newOngo.LOT_DATE_START = itemSettings.START_DATE;
                        newOngo.LOT_DATE_END = itemSettings.END_DATE;
                    }
                }
            }
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
        public void ReUnitOnGo(decimal unitBuy, decimal unitSell, decimal unitTranIn, decimal unitTranOut, DateTime navDate)
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

            var unitRemove = unitSell + unitTranOut;
            this.UNIT_OUT = unitSell + unitTranOut;
            this.UNIT_OUT_ONGO = 0;

            // ลด
            while (unitRemove > 0)
            {
                feeOngo = this.OngoDetails.OrderBy(m => m.LOT_DATE_START).FirstOrDefault();
                if (feeOngo != null)
                {
                    if (unitRemove > feeOngo.UNIT_BY_LOT)
                    {
                        // แบบเหลือ unit
                        this.UNIT_OUT_ONGO += unitRemove - feeOngo.UNIT_BY_LOT;
                        unitRemove -= feeOngo.UNIT_BY_LOT;
                        this.OngoDetails.Remove(feeOngo);
                    }
                    else
                    {
                        // แบบ unit หมด
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
        }

        /// <summary>
        ///  ปรับค่า bfrmf เฉพาะกองพิเศษ โดยที่เมื่อมีการโอนเข้าจากผู้โอน ที่ agent และ fund เดียวกัน ถ้าผู้โอนมีค่า bfrmf ต้องนำมาบวกค่า bf ให้กับ marketing คนนี้ด้วย
        ///  จำนวนโอนเข้าคิดเป็นสัดส่วนเทียบกับค่า bf
        /// </summary>
        /// <param name="traferInData">ข้อมูลการโอนเข้าทังหมด</param>
        /// <param name="feeDateBefore">fee วันก่อนหน้าของ กอง และ ตัวแทนเดียวกัน</param>
        public void ReBfValueFromTranIn(List<OPNTTRAN> traferInData, FeeAgentDaily feeDateBefore)
        {
            if (this.OnDateAgentFee.SettingOwner.FundOwner.IsExtraFund)
            {
                // ต้องเป็นกองพิเศษ
                if (this.UNIT_TRANIN > 0)
                {
                    // หา mkt โอนออกมาให้ ต้องมีค่า bf
                    var mktTranIns = traferInData.Where(q => q.FUND_CODE == this.OnDateAgentFee.FUND_ID)
                                         .Where(q => q.BR_CODE == this.OnDateAgentFee.AGENT_ID)
                                         .Where(q => q.TRAN_DATE == this.OnDateAgentFee.FEE_DATE)
                                         .Where(q => q.MKT_CODE == this.MKT)
                                         .Where(q => q.SHARE_UNIT > 0)
                                         .ToList();

                    decimal BfSum = 0;
                    foreach (var itemTranIn in mktTranIns)
                    {
                        var mktTranOutCode = itemTranIn.MKT_CODE_OLD;
                        var feeDailyBfFromMktTranOut = feeDateBefore.GetMarktingByCode(mktTranOutCode);

                        if (feeDailyBfFromMktTranOut != null)
                        {
                            if (feeDailyBfFromMktTranOut.UNIT_BF > 0)
                            {
                                // คนโอนต้องมีค่า bf ถ้าไมมีก็ไม่จำเป็นต้องคำนวน
                                BfSum = BfSum + ((decimal)itemTranIn.SHARE_UNIT / (decimal)feeDailyBfFromMktTranOut.TOTAL_UNIT_ONGO) * feeDailyBfFromMktTranOut.UNIT_BF;
                            }
                        }
                    }

                    this.UNIT_BF = this.UNIT_BF + BfSum;
                }
            }
        }

        /// <summary>
        /// ปรับค่า bfrmf เฉพาะกองพิเศษ โดยที่เมื่อมีการโอนออกจากผู้โอน ที่ agent และ fund เดียวกัน 
        /// จำนวนโอนออกคิดเป็นสัดส่วนเทียบกับค่า bf
        /// </summary>
        /// <param name="traferOutData">ข้อมูลการโอนออกทังหมด</param>
        /// <param name="feeDateBefore">fee วันก่อนหน้าของ กอง และ ตัวแทนเดียวกัน</param>
        public void ReBfValueFromTranOut(List<OPNTTRAN> traferOutData, FeeAgentDaily feeDateBefore)
        {
            if (this.OnDateAgentFee.SettingOwner.FundOwner.IsExtraFund)
            {
                // ต้องเป็นกองพิเศษ
                if (this.UNIT_TRANOUT > 0 & this.UNIT_BF > 0)
                {
                    // หา mkt โอนออกมาให้ และ ต้องมีค่า bf 
                    var mktTranOuts = traferOutData.Where(q => q.FUND_CODE == this.OnDateAgentFee.FUND_ID)
                                         .Where(q => q.BR_CODE_OLD == this.OnDateAgentFee.AGENT_ID)
                                         .Where(q => q.TRAN_DATE == this.OnDateAgentFee.FEE_DATE)
                                         .Where(q => q.MKT_CODE_OLD == this.MKT)
                                         .Where(q => q.SHARE_UNIT > 0)
                                         .ToList();

                    decimal BfSum = 0;
                    foreach (var itemTranOut in mktTranOuts)
                    {
                        var mktTranInCode = itemTranOut.MKT_CODE;
                        var feeDailyBfFromMktTranIn = feeDateBefore.GetMarktingByCode(mktTranInCode);

                        if (feeDailyBfFromMktTranIn != null)
                        {
                            BfSum = BfSum + ((decimal)itemTranOut.SHARE_UNIT / (decimal)this.TOTAL_UNIT_ONGO_BEFORE_DATE) * this.UNIT_BF_BEFORE_DATE;
                        }
                    }

                    this.UNIT_BF = (this.UNIT_BF - BfSum) < 0 ? 0 : this.UNIT_BF - BfSum;
                }
            }
        }

        /// <summary>
        /// คำนวน unit ในแต่ละปีใหม่ 
        /// </summary>
        public void ReUnitYear()
        {
            // เราไม่สามารถเอายอด เข้าออกมาคำนวนเลยได้ เราต้องใช้ยอด div ของ mkt ระหว่างวันแทน
            this.UNIT_ADD_ONYEAR = 0;
            this.UNIT_OUT_ONYEAR = 0;

            var difUnit = (decimal)this.TOTAL_UNIT_ONGO - this.TOTAL_UNIT_ONGO_BEFORE_DATE;
            if (difUnit > 0)
            {
                // ถ้า unit mkt เพิ่มนำเข้า
                var feeOnyear = this.OnYears.Where(m => m.YEAR_ORDER == "1").FirstOrDefault();
                if (feeOnyear != null)
                {
                    this.UNIT_ADD_ONYEAR = difUnit;
                    feeOnyear.UNIT_BY_YEAR += difUnit;
                }
            }

            if (difUnit < 0)
            {
                // ถ้า unit mkt ลดนำออก
                var unitOut = Math.Abs(difUnit);

                while (unitOut > 0)
                {
                    // ปรับลดเฉพาะ lot ที่มี unit และเป็นปีที่ เยอะที่สุดก่อน
                    var feeOnyear = this.OnYears.Where(m => m.UNIT_BY_YEAR > 0).OrderByDescending(m => m.YEAR_ORDER).FirstOrDefault();
                    if (feeOnyear != null)
                    {
                        if (unitOut > feeOnyear.UNIT_BY_YEAR)
                        {
                            // แบบเหลือ unit
                            this.UNIT_OUT_ONYEAR = this.UNIT_OUT_ONYEAR + feeOnyear.UNIT_BY_YEAR;
                            unitOut = unitOut - feeOnyear.UNIT_BY_YEAR;
                            this.OnYears.Remove(feeOnyear);
                        }
                        else
                        {
                            // แบบ unit หมด
                            this.UNIT_OUT_ONYEAR = this.UNIT_OUT_ONYEAR + unitOut;
                            feeOnyear.UNIT_BY_YEAR = feeOnyear.UNIT_BY_YEAR - unitOut;
                            unitOut = 0;
                        }
                    }
                    else
                    {
                        // ถ้าไม่เจอ lot ก็ให้ออกไปเลย
                        unitOut = 0;
                    }
                }
            }
        }

        /// <summary>
        /// ปรับจำนวนหน่วยใน lot ใหม่โดยหักจาก bf ออกไปถ้ามีค่า bf
        /// </summary>
        public void ReUnitOngoWithBfValue()
        {
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

        /// <summary>
        /// คำนวน onpay ต้องเป็นตัวแทน tbank และมีการตั้งค่้าส่วน onpay
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public decimal?  CalculateFeeOnPay()
        {

            var setting = this.OnDateAgentFee.SettingOwner;

            if (setting != null)
            {
                var settingOnPay = ((FEE_SETTING_TBANK)setting).GetOngoOnPaySettingByCondition(this.OnDateAgentFee.FEE_DATE);

                this.RATE_ONGO_ONPAY_USED = settingOnPay.RateMktCalculated;
                this.TOTAL_FEE_ONGO = this.TOTAL_FEE_ONGO ?? 0; // ไม่แน่ใจว่ามีโอกาศเป็น null บ้างมั้ยใน case ที่มี ongo แล้วนะ
                this.TOTAL_FEE_ONGO_ONPAY = ((decimal)this.TOTAL_FEE_ONGO * (decimal)this.RATE_ONGO_ONPAY_USED).WithoutRounding();
            }

            return this.TOTAL_FEE_ONGO_ONPAY;
        }

        public decimal CalculateFeeOngo()
        {
            this.TOTAL_FEE_ONGO = this.OngoDetails.Sum(m => m.CalculateFee());

            return (decimal)this.TOTAL_FEE_ONGO;
        }

        public decimal CalculateFeeOnYear()
        {
            this.TOTAL_FEE_ONGO_ONYEAR = this.OnYears.Sum(m => m.CalculateFee());

            return (decimal)this.TOTAL_FEE_ONGO_ONYEAR;
        }

        /// <summary>
        /// ทำการคำนวน fee reward โดยจะมีขึ้นตอนประมาณนี้
        /// 1. คำนวน fee onpay
        /// 2. re lot year 
        /// 3. re unit year
        /// 4. re level
        /// 5. calculate fee reward
        /// </summary>
        /// <returns></returns>
        public decimal CalculateFeeOngoReward()
        {
            // ต้องเป็น tbank ถึงจะมีการคำนวน on pay
            if (this.OnDateAgentFee.SettingOwner.AgentOwner.IsTBank)
            {
                this.CalculateFeeOnPay();
                this.ReLotYearFormSetting();
                this.ReUnitYear();

                // ปรับ level ตามที่ตั้งค่าไว้
                foreach (var itemYear in this.OnYears)
                {
                    itemYear.ReLevelFromSetting();
                }

                this.CalculateFeeOnYear();
                this.TOTAL_FEE_ONGO_REWARD = this.OnYears.Sum(m => m.CalculateFeeReward());

                return (decimal)this.TOTAL_FEE_ONGO_REWARD;
            }
            else
            {
                return 0;
            }
        }

        public decimal CalculateFeeUpFront()
        {
            this.UpFrontDetails.Sum(m => m.CalculateFee());
            this.TOTAL_FEE_UPFRONT = this.UpFrontDetails.Where(m => m.IS_PAID).Sum(m => m.FEE);
            this.TOTAL_FEE_UPFRONT_NOTPAID = this.UpFrontDetails.Where(m => !m.IS_PAID).Sum(m => m.FEE);

            return this.TOTAL_FEE_UPFRONT ?? 0;
        }

        public decimal CalculateFeeUpFrontReward()
        {
            if (this.OnDateAgentFee.SettingOwner.AgentOwner.IsTBank)
            {
                // get setting
                var setting = this.OnDateAgentFee.SettingOwner;
                if (setting != null)
                {

                    // get rate onpay
                    FEE_SETTING_UPFRONT_ONPAY settingOnpay = ((FEE_SETTING_TBANK)setting).GetUpFrontOnPaySettingByCondition(this.FEE_DATE);
                    if (settingOnpay != null)
                    {
                        this.RATE_UPFRONT_ONPAY_USED = settingOnpay.RateMktCalculated;
                    }
                    else
                    {
                        this.RATE_UPFRONT_ONPAY_USED = 0m;
                    }

                    // get rate twealth
                    var settingTWealth = ((FEE_SETTING_TBANK)setting).GetUpFrontTWealth();
                    if (settingTWealth.Any())
                    {
                        this.RATE_UPFRONT_TWEALTH_USED = settingTWealth.First().RateMktCalculated; // จิงๆเป็น 1 ต่อ หนึ่ง แต่ออกแบบมาเผื่อไว้ก่อน
                    }
                    else
                    {
                        this.RATE_UPFRONT_TWEALTH_USED = 0m;
                    }

                    // calculate reward for each upfront
                    foreach (var itemUpFront in this.UpFrontDetails)
                    {
                        itemUpFront.ReLevelFromSetting();
                        itemUpFront.CalculateFeeOnpay();
                        itemUpFront.CalculateFeeReward();
                    }

                    this.TOTAL_FEE_UPFRONT_REWARD = this.UpFrontDetails.Sum(m => m.TOTAL_FEE_REWARD);
                }
            }

            return this.TOTAL_FEE_UPFRONT_REWARD ?? 0;
        }


        #endregion
    }
}
