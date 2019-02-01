using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models
{

    public interface IFeeSetting
    {
        /// <summary>
        /// เลือก fee ongo ตามเงื่อนไขที่ควรจะเป็น
        /// </summary>
        /// <param name="dateFee"></param>
        /// <param name="netAmount"></param>
        /// <returns></returns>
        FEE_SETTING_ONGO GetOnGoSettingByCondition(DateTime dateFee, decimal netAmount);

        /// <summary>
        /// เรียก Lotdate ที่มีวันที่เริ่มต้นน้อยเท่ากับวันที่ส่งเข้ามา
        /// </summary>
        /// <param name="feeDate"></param>
        /// <returns></returns>
        List<FEE_SETTING_ONGO> GetOngoLotDateLessThanByDate(DateTime feeDate);

        FeeAgentDaily NewFeeDailyByDate(DateTime feeDate, DateTime feeBeforeDate);

        FeeAgentDaily GetFeeDailyByDate(DateTime feeDate);

        decimal GetUnitOngoFromLastDailyFee();

        decimal SumFeeOngoAgentByDate(DateTime startDate,DateTime endDate);

    }

    [Table("CIS.FEE_SETTING")]
    public class FEE_SETTING : IFeeSetting
    {

        public FEE_SETTING()
        {
            this.FES_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }

        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [StringLength(36)]
        public string FES_ID { get; set; }

        /// <summary>
        /// FK
        /// </summary>
        [ForeignKey("FundOwner")]
        [StringLength(30)]
        [Column("FUND", Order = 0)]
        public string FUND_ID { get; set; }
        public virtual Fund FundOwner { get; set; }

        /// <summary>
        /// FK
        /// </summary>
        [ForeignKey("AgentOwner")]
        [StringLength(9)]
        [Column("AGENT", Order = 1)]
        public string AGENT_ID { get; set; }
        private Agent _AgentOwner;
        public virtual Agent AgentOwner
        {
            get
            {
                //if (this.AGENT_ID != null)
                //{
                //    if (IsAllAgent)
                //    {
                //        return new Agent() { BR_CODE = "000", BR_NAME = "ทุกตัวแทน" };
                //    }
                //}
                return _AgentOwner;
            }
            set
            {
                _AgentOwner = value;
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
        /// เป็นการตั้งค่าของทุก agent?
        /// </summary>
        public bool IsAllAgent
        {
            get
            {
                if (this.AGENT_ID == "000")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #region Relation

        private ICollection<FEE_SETTING_ONGO> _SettingOngos = new HashSet<FEE_SETTING_ONGO>();
        public virtual ICollection<FEE_SETTING_ONGO> SettingOngos
        {
            get { return _SettingOngos; }
            set { _SettingOngos = value; }
        }

        private ICollection<FEE_SETTING_UPFRONT> _SettingUpFronts = new HashSet<FEE_SETTING_UPFRONT>();
        public virtual ICollection<FEE_SETTING_UPFRONT> SettingUpFronts
        {
            get { return _SettingUpFronts; }
            set { _SettingUpFronts = value; }
        }

        private ICollection<FeeAgentDaily> _FeeDailyAgents = new HashSet<FeeAgentDaily>();
        public virtual ICollection<FeeAgentDaily> FeeDailyAgents
        {
            get { return _FeeDailyAgents; }
            set { _FeeDailyAgents = value; }
        }

        private ICollection<FeeMarketing> _FeeDailyMarketings = new HashSet<FeeMarketing>();
        public virtual ICollection<FeeMarketing> FeeDailyMarketings
        {
            get { return _FeeDailyMarketings; }
            set { _FeeDailyMarketings = value; }
        }

        #endregion

        #region Method

        public FeeAgentDaily GetFeeDailyByDate(DateTime feeDate)
        {
            return this.FeeDailyAgents.SingleOrDefault(m => m.FEE_DATE == feeDate);
        }

        public FEE_SETTING_ONGO GetOnGoSettingByCondition(DateTime lotDateStart, decimal netAmount)
        {
            var ongoSetting = this.SettingOngos
                              .Where(q => q.START_DATE == lotDateStart)
                              .Where(q => q.NET_AMOUNT <= netAmount)
                                .OrderByDescending(q => q.NET_AMOUNT).FirstOrDefault();

            return ongoSetting;
        }

        public FEE_SETTING_UPFRONT_CHANNEL GetUpFrontSettingByCondition(DateTime feeDate, string channel)
        {
            FEE_SETTING_UPFRONT upFrontSetting = this.SettingUpFronts
                             .SingleOrDefault(q => (q.START_DATE <= feeDate && feeDate <= q.END_DATE) ||
                                                   (q.START_DATE <= feeDate && q.END_DATE == null));

            if (upFrontSetting != null)
            {
               return  upFrontSetting.Channels.SingleOrDefault(m => m.CHANNEL == channel);
            }

            return null;
        }

        /// <summary>
        /// เรียกข้อมูล การตั้งค่าที่น้อยกว่่า หรือ เท่ากับวันที่ fee (จะได้ Lot ที่ควรนำมาใช้)
        /// </summary>
        /// <param name="feeDate"></param>
        /// <returns></returns>
        public List<FEE_SETTING_ONGO> GetOngoLotDateLessThanByDate(DateTime feeDate)
        {
            var filterDates = this.SettingOngos.Where(q => q.START_DATE <= feeDate).ToList();
            var groupDates = filterDates.GroupBy(m => new { m.START_DATE, m.END_DATE },
                                          (key, group) => new FEE_SETTING_ONGO()
                                          {
                                              START_DATE = key.START_DATE,
                                              END_DATE = key.END_DATE
                                          }).ToList();

            return groupDates;
        }

        public decimal GetUnitOngoFromLastDailyFee()
        {

            FeeAgentDaily findFeeAgent = this.FeeDailyAgents.OrderByDescending(m => m.FEE_DATE)
                                                            .FirstOrDefault() ?? new FeeAgentDaily();
            return findFeeAgent.TOTAL_UNIT_ONGO ?? 0;
        }

        public decimal GetUnitOngoFromLastDailyByMaketing(string mkt)
        {
            FeeMarketing findFeeMkt = this.FeeDailyMarketings.Where(m => m.MKT == mkt)
                                           .OrderByDescending(m => m.FEE_DATE)
                                           .FirstOrDefault() ?? new FeeMarketing();
            return findFeeMkt.TOTAL_UNIT_ONGO ?? 0;
        }

        public FEE_SETTING_ONGO NewOngoSetting()
        {
            FEE_SETTING_ONGO NewData = new FEE_SETTING_ONGO();
            NewData.FES_ID = this.FES_ID;
            NewData.MKT_RATE = 100;
            NewData.DataStatus = EnumDataStatus.NewData;

            this.SettingOngos.Add(NewData);

            return NewData;
        }

        public FEE_SETTING_UPFRONT NewUpFrontSetting()
        {
            FEE_SETTING_UPFRONT NewData = new FEE_SETTING_UPFRONT();
            NewData.FES_ID = this.FES_ID;
            NewData.DataStatus = EnumDataStatus.NewData;
            NewData.SettingOwner = this;

            this.SettingUpFronts.Add(NewData);

            return NewData;
        }

        public FeeAgentDaily NewFeeDailyByDate(DateTime feeDate)
        {
            FeeAgentDaily newItem = new FeeAgentDaily();
            newItem.FES_ID = this.FES_ID;
            newItem.SettingOwner = this;
            newItem.FUND_ID = this.FUND_ID;
            newItem.AGENT_ID = this.AGENT_ID;
            newItem.FEE_DATE = feeDate;
            newItem.FEE_BEFORE_DATE = feeDate;

            this.FeeDailyAgents.Add(newItem);

            return newItem;
        }

        public FeeAgentDaily NewFeeDailyByDate(DateTime feeDate, DateTime feeBeforeDate)
        {
            FeeAgentDaily newItem = new FeeAgentDaily();
            newItem.FES_ID = this.FES_ID;
            newItem.SettingOwner = this;
            newItem.FUND_ID = this.FUND_ID;
            newItem.AGENT_ID = this.AGENT_ID;
            newItem.FEE_DATE = feeDate;
            newItem.FEE_BEFORE_DATE = feeBeforeDate;

            this.FeeDailyAgents.Add(newItem);

            return newItem;
        }

        public FEE_SETTING_UPFRONT AddjustUpFrontSetting(DateTime startDate, DateTime? endDate)
        {
           var findUpFront = this.SettingUpFronts.SingleOrDefault(m => m.START_DATE == startDate && m.END_DATE == endDate);

           if (findUpFront == null)
           {
               findUpFront = this.NewUpFrontSetting();
               findUpFront.START_DATE = startDate;
               findUpFront.END_DATE = endDate;

           }
           return findUpFront;
        }


        public decimal SumFeeOngoAgentByDate(DateTime startDate, DateTime endDate)
        {
            return this.FeeDailyAgents
                           .Where(m => m.FEE_DATE >= startDate)
                           .Where(m => m.FEE_DATE <= endDate)
                                .Sum(m => m.TOTAL_FEE_ONGO ?? 0);
        }

        public decimal SumFeeOngoMarketingByDate(DateTime startDate, DateTime endDate)
        {
            return this.FeeDailyMarketings
                             .Where(m => m.FEE_DATE >= startDate)
                             .Where(m => m.FEE_DATE <= endDate)
                                .Sum(m => m.TOTAL_FEE_ONGO ?? 0);
        }

        /// <summary>
        /// sum fee ongo marketing group by fund, agent, mkt (ผลลัพธ์ให้ใช้คอลัม MKT,TOTAL_FEE_ONGO)
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<FeeMarketing> SumFeeOngoGroupMarketingByDate(DateTime startDate,DateTime endDate)
        {
            var result = this.FeeDailyMarketings
                                          .Where(m => m.FEE_DATE >= startDate)
                                          .Where(m => m.FEE_DATE <= endDate)
                                          .GroupBy(m => new { m.FES_ID, m.MKT },
                                              (key, group) => new FeeMarketing()
                                              {
                                                  MKT = key.MKT,
                                                  TOTAL_FEE_ONGO = group.Sum(m => m.TOTAL_FEE_ONGO),
                                                  OfMarketing = group.First().OfMarketing
                                              }).ToList();

            return result;
        }


        #endregion


    }



}
