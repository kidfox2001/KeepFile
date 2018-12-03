using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data.Entity.Infrastructure;
using TFundSolution.Models;

namespace TFundSolution.Services
{

    // end date
    // ป้องกันการกด reload page
    // เหลือ 2 page
    // authen
    // maker checker
    // tracking controler
    // ทำให้สวย
    // validate ถ้าได้ดีกว่านี้ก็ดีเรื่องวันที่

    public partial class ModelTFundCis : DbContext
    {
        public ModelTFundCis()
            : base("name=ModelCis")
        {
            Database.SetInitializer<ModelTFundCis>(null);
            //GenScript();
        }

        public virtual DbSet<Agent> OPNMAGEN { get; set; }
        public virtual DbSet<Fund> OPNCONTL { get; set; }
        public virtual DbSet<LOG_SYSTEM> LOG_SYSTEM { get; set; }
        public virtual DbSet<FEE_SETTING> FEE_SETTING { get; set; }
        public virtual DbSet<FEE_SETTING_TBANK> FEE_SETTING_TBANK { get; set; }
        public virtual DbSet<FEE_SETTING_ONGO> FEE_SETTING_ONGO { get; set; }
        public virtual DbSet<FEE_SETTING_ONPAY> FEE_SETTING_ONPAY { get; set; }
        public virtual DbSet<FEE_SETTING_YEAR> FEE_SETTING_YEAR { get; set; }
        public virtual DbSet<FEE_SETTING_ONLEVEL> FEE_SETTING_ONLEVEL { get; set; }
        public virtual DbSet<FEE_SETTING_UPFRONT> FEE_SETTING_UPFRONT { get; set; }
        public virtual DbSet<FEE_UPFRONT_ONLEVEL> FEE_UPFRONT_ONLEVEL { get; set; }
        public virtual DbSet<V_CALENDAR> V_CALENDAR { get; set; }
        public virtual DbSet<DailyNav> V_W_NAV { get; set; }
        public virtual DbSet<OPNTTRAN> OPNTTRAN { get; set; }
        public virtual DbSet<Marketing> OPNMMAKT { get; set; }
        public virtual DbSet<OPNMFEEAGENT> OPNMFEEAGENT { get; set; }
        public virtual DbSet<OPNMFEEMKT> OPNMFEEMKT { get; set; }
        public virtual DbSet<FeeAgentDaily> FeeAgentDaily { get; set; }
        public virtual DbSet<OPNMFEEONPAY> OPNMFEEONPAY { get; set; }
        public virtual DbSet<OPNMFEEREWARD> OPNMFEEREWARD { get; set; }
 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FEE_SETTING>().Map(x => x.Requires("DISCRIMINATOR").HasValue("FEE_SETTING"));
            modelBuilder.Entity<FEE_SETTING_TBANK>().Map(x => x.Requires("DISCRIMINATOR").HasValue("FEE_SETTING_TBANK"));

            modelBuilder.Entity<AgentBfValue>().Property(x => x.BF_VALUE).HasPrecision(14, 4);
            //modelBuilder.Entity<MarketingBfValue>().Property(x => x.BF_VALUE).HasPrecision(14, 4);

            modelBuilder.Entity<FeeAgentDaily>().Property(x => x.UNIT_BUY).HasPrecision(14, 4);
            modelBuilder.Entity<FeeAgentDaily>().Property(x => x.UNIT_SELL).HasPrecision(14, 4);
            modelBuilder.Entity<FeeAgentDaily>().Property(x => x.UNIT_TRANIN).HasPrecision(14, 4);
            modelBuilder.Entity<FeeAgentDaily>().Property(x => x.UNIT_TRANOUT).HasPrecision(14, 4);
            modelBuilder.Entity<FeeAgentDaily>().Property(x => x.FUND_NET_AMOUNT).HasPrecision(14, 2);
            modelBuilder.Entity<FeeAgentDaily>().Property(x => x.FUND_NET_SHARE).HasPrecision(14, 4);
            modelBuilder.Entity<FeeAgentDaily>().Property(x => x.UNIT_ADD).HasPrecision(14, 4);
            modelBuilder.Entity<FeeAgentDaily>().Property(x => x.UNIT_OUT).HasPrecision(14, 4);
            modelBuilder.Entity<FeeAgentDaily>().Property(x => x.UNIT_ADD_ONGO).HasPrecision(14, 4);
            modelBuilder.Entity<FeeAgentDaily>().Property(x => x.UNIT_OUT_ONGO).HasPrecision(14, 4);
            modelBuilder.Entity<FeeAgentDaily>().Property(x => x.UNIT_BF).HasPrecision(14, 4);
            modelBuilder.Entity<FeeAgentDaily>().Property(x => x.TOTAL_UNIT_ONGO).HasPrecision(14, 4);
            modelBuilder.Entity<FeeAgentDaily>().Property(x => x.TOTAL_FEE_ONGO).HasPrecision(14, 2);


            modelBuilder.Entity<FeeMarketing>().Property(x => x.UNIT_BUY).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.UNIT_SELL).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.UNIT_TRANIN).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.UNIT_TRANOUT).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.UNIT_ADD).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.UNIT_OUT).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.UNIT_ADD_ONGO).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.UNIT_OUT_ONGO).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.UNIT_ADD_ONYEAR).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.UNIT_OUT_ONYEAR).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.UNIT_BF).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.UNIT_BF_BEFORE_DATE).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.TOTAL_UNIT_ONGO).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.TOTAL_UNIT_ONGO_BEFORE_DATE).HasPrecision(14, 4);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.TOTAL_FEE_ONPAY).HasPrecision(14, 2);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.TOTAL_FEE_ONGO).HasPrecision(14, 2);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.TOTAL_FEE_ONYEAR).HasPrecision(14, 2);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.TOTAL_FEE_REWARD).HasPrecision(14, 2);
            modelBuilder.Entity<FeeMarketing>().Property(x => x.RATE_ONPAY_USED).HasPrecision(7, 5);


            modelBuilder.Entity<FeeOngoAgent>().Property(x => x.UNIT_BY_LOT).HasPrecision(14, 4);
            modelBuilder.Entity<FeeOngoAgent>().Property(x => x.UNIT_FOR_CAL).HasPrecision(14, 4);
            modelBuilder.Entity<FeeOngoAgent>().Property(x => x.FEE_BY_LOT).HasPrecision(14, 2);
            modelBuilder.Entity<FeeOngoAgent>().Property(x => x.RATE_USED).HasPrecision(7, 5);


            modelBuilder.Entity<FeeOngoMarketing>().Property(x => x.UNIT_BY_LOT).HasPrecision(14, 4);
            modelBuilder.Entity<FeeOngoMarketing>().Property(x => x.UNIT_FOR_CAL).HasPrecision(14, 4);
            modelBuilder.Entity<FeeOngoMarketing>().Property(x => x.FEE_BY_LOT).HasPrecision(14, 2);
            modelBuilder.Entity<FeeOngoMarketing>().Property(x => x.RATE_USED).HasPrecision(7, 5);


            modelBuilder.Entity<FeeOnYearMarketing>().Property(x => x.UNIT_BY_YEAR).HasPrecision(14, 4);
            modelBuilder.Entity<FeeOnYearMarketing>().Property(x => x.FEE_BY_YEAR).HasPrecision(14, 2);
            modelBuilder.Entity<FeeOnYearMarketing>().Property(x => x.TOTAL_FEE_BY_LEVEL).HasPrecision(14, 2);


            modelBuilder.Entity<FeeRewardMarketing>().Property(x => x.FEE_BY_LEVEL).HasPrecision(14, 2);
            modelBuilder.Entity<FeeRewardMarketing>().Property(x => x.RATE_USED).HasPrecision(7, 5);


            modelBuilder.Entity<FEE_SETTING_ONGO>().Property(x => x.AGENT_RATE).HasPrecision(12, 4);
            modelBuilder.Entity<FEE_SETTING_ONGO>().Property(x => x.MKT_RATE).HasPrecision(12, 4);


            modelBuilder.Entity<FEE_SETTING_ONPAY>().Property(x => x.AGENT_RATE).HasPrecision(12, 4);
            modelBuilder.Entity<FEE_SETTING_ONPAY>().Property(x => x.MKT_RATE).HasPrecision(12, 4);


            modelBuilder.Entity<FEE_SETTING_ONLEVEL>().Property(x => x.RATE).HasPrecision(12, 4);


            modelBuilder.Entity<FEE_SETTING_UPFRONT>().Property(x => x.RATE).HasPrecision(12, 4);


            modelBuilder.Entity<FEE_UPFRONT_ONLEVEL>().Property(x => x.RATE).HasPrecision(12, 4);


            #region OPNCONTL

            modelBuilder.Entity<Fund>()
              .Property(e => e.FUND)
              .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.HOLDER)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.CISNO)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.CERNO)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.NAV)
                .HasPrecision(7, 4);

            modelBuilder.Entity<Fund>()
                .Property(e => e.FLG_NEW)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.FLG_PRN)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.FUND_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.FUND_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.MIN_SHARE)
                .HasPrecision(14, 4);

            modelBuilder.Entity<Fund>()
                .Property(e => e.FLG_CER)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.DOC_NO)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.BANK_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.BANK_ADDR)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.BANK_TEL)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.MOR_NO)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.SIGN_AMT)
                .HasPrecision(14, 2);

            modelBuilder.Entity<Fund>()
                .Property(e => e.BUY_MIN_SHARE)
                .HasPrecision(14, 4);

            modelBuilder.Entity<Fund>()
                .Property(e => e.SELL_MIN_SHARE)
                .HasPrecision(14, 4);

            modelBuilder.Entity<Fund>()
                .Property(e => e.BUY_NEXT)
                .HasPrecision(14, 4);

            modelBuilder.Entity<Fund>()
                .Property(e => e.SWITCH_OUT)
                .HasPrecision(14, 4);

            modelBuilder.Entity<Fund>()
                .Property(e => e.SWITCH_IN)
                .HasPrecision(14, 4);

            modelBuilder.Entity<Fund>()
                .Property(e => e.NAV_BUY)
                .HasPrecision(7, 4);

            modelBuilder.Entity<Fund>()
                .Property(e => e.NAV_SELL)
                .HasPrecision(7, 4);

            modelBuilder.Entity<Fund>()
                .Property(e => e.UPD_BY)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.SELL_MIN_AMT)
                .HasPrecision(14, 2);

            modelBuilder.Entity<Fund>()
                .Property(e => e.BUY_NEXT_BATH)
                .HasPrecision(14, 2);

            modelBuilder.Entity<Fund>()
                .Property(e => e.MIN_BATH)
                .HasPrecision(14, 2);

            modelBuilder.Entity<Fund>()
                .Property(e => e.ORDER_SB)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.ORDER_RD)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.CLASS_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.TYPE_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.MATCH_NO)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.AUDIT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.AUDIT_COMP)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.FUND_COND)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.FUND_POLICY)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.FUND_RETURN)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.FEE_MANAGEMENT)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Fund>()
                .Property(e => e.FEE_TRUSTEE)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Fund>()
                .Property(e => e.TAX_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.REDM_FREQ)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.REDM_DAY_OF_WEEK)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.START_DAY_OF_MONTH)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.END_DAY_OF_MONTH)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.TRADE_START_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.TRADE_END_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.DIVIDENT_FREQ)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.BUY_FREQ)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.BUY_DAY_OF_WEEK)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.BSTART_DAY_OF_MONTH)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.BEND_DAY_OF_MONTH)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.BF_AMOUNT)
                .HasPrecision(14, 2);

            modelBuilder.Entity<Fund>()
                .Property(e => e.MATURITY)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.MERCHANT_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.AR_MIN_BATH)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Fund>()
                .Property(e => e.SEND_WEB)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.AR_FLG)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.IVR_FLG)
                .IsUnicode(false);

            modelBuilder.Entity<Fund>()
                .Property(e => e.AR_PERCENT)
                .HasPrecision(3, 2);

            modelBuilder.Entity<Fund>()
                .Property(e => e.SW_NEXT_BATH)
                .HasPrecision(14, 2);

            modelBuilder.Entity<Fund>()
                .Property(e => e.SW_NEXT_SHARE)
                .HasPrecision(14, 4);

            #endregion

            #region OPNMAGEN
            modelBuilder.Entity<Agent>()
                    .Property(e => e.BR_CODE)
                    .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.BR_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.BR_ADDR1)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.BR_ADDR2)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.BR_ADDR3)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.BR_ZIP)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.BR_PROV)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.NEW_ORDER)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.NEW_CISNO)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.GROUP_FUND)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.NEW_ORDER_IPO)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.REGIS_NO)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.REGIS_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.BR_FLAG)
                .IsUnicode(false);
            #endregion

            #region V_CALENDAR
            modelBuilder.Entity<V_CALENDAR>()
                  .Property(e => e.DAY_WEEK)
                  .HasPrecision(38, 0);

            modelBuilder.Entity<V_CALENDAR>()
                .Property(e => e.DAY_MONTH)
                .HasPrecision(38, 0);

            modelBuilder.Entity<V_CALENDAR>()
                .Property(e => e.DAY_YEAR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<V_CALENDAR>()
                .Property(e => e.MONTH)
                .HasPrecision(38, 0);

            modelBuilder.Entity<V_CALENDAR>()
                .Property(e => e.WEEK_MONTH)
                .HasPrecision(38, 0);

            modelBuilder.Entity<V_CALENDAR>()
                .Property(e => e.WEEK_YEAR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<V_CALENDAR>()
                .Property(e => e.YEAR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<V_CALENDAR>()
                .Property(e => e.HOLIDAY_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<V_CALENDAR>()
                .Property(e => e.DESCRIPTION_DATE)
                .IsUnicode(false);
            #endregion

            #region V_W_NAV
            modelBuilder.Entity<DailyNav>()
                    .Property(e => e.CLIENT_CODE)
                    .IsUnicode(false);

            modelBuilder.Entity<DailyNav>()
                .Property(e => e.TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<DailyNav>()
                .Property(e => e.SUBTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<DailyNav>()
                .Property(e => e.ISSUESTOCK)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DailyNav>()
                .Property(e => e.NAV)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DailyNav>()
                .Property(e => e.BID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DailyNav>()
                .Property(e => e.OFFER)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DailyNav>()
                .Property(e => e.NAV_AMT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DailyNav>()
                .Property(e => e.BID30)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DailyNav>()
                .Property(e => e.BID90)
                .HasPrecision(38, 0);
            #endregion

            #region OPNTTRAN
            modelBuilder.Entity<OPNTTRAN>()
                    .Property(e => e.TRAN_FLG)
                    .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.FUND_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.CIS_NO)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.HOLDER_ID)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.SHARE_AMT)
                .HasPrecision(14, 2);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.BENEFIT_AMT)
                .HasPrecision(14, 2);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.SHARE_UNIT)
                .HasPrecision(16, 4);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.COMP_RECV)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.FUND_RECV)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.NAV_AVG)
                .HasPrecision(24, 14);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.ACCT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.BR_CODE_OLD)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.MKT_CODE_OLD)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.BR_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.MKT_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.TOT_SHARE)
                .HasPrecision(14, 4);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.FLAG_UP)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.UPD_BY)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.CER_NO)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.CIS_TRAN)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.ORDER_NO)
                .IsUnicode(false);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.TAX_BENEFIT)
                .HasPrecision(14, 2);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.REAL_COST)
                .HasPrecision(24, 10);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.REAL_BENEFIT)
                .HasPrecision(24, 10);

            modelBuilder.Entity<OPNTTRAN>()
                .Property(e => e.SELL_CONDITION)
                .IsUnicode(false);
            #endregion


            #region OPNMAGEN
            modelBuilder.Entity<Marketing>()
                    .Property(e => e.MKT_AGEN)
                    .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.MKT_BRANCH)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.MKT_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.MKT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.ID_PASS)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.GRP_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.MKT_DEPT)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.PRIVATE_FLAG)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.GRP_TYPE_PRIVATE)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.RESIGN)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.TARGET)
                .HasPrecision(14, 4);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.IP_LEVEL)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.IP_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.PVD_FLAG)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.GRP_TYPE_PVD)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.RESIGN_FLG)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.BRANCH_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<Marketing>()
                .Property(e => e.UPD_BY)
                .IsUnicode(false);
            #endregion

            //#region OPNMFEEMKT
            //modelBuilder.Entity<OPNMFEEMKT>()
            //        .Property(e => e.FUND)
            //        .IsUnicode(false);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.AGENT)
            //    .IsUnicode(false);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.MKT)
            //    .IsUnicode(false);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.FEE_AMT)
            //    .HasPrecision(14, 2);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.TOT_UNI)
            //    .HasPrecision(14, 4);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.MKT_UNI)
            //    .HasPrecision(14, 4);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.MKT_FEE)
            //    .HasPrecision(14, 2);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.UPD_BY)
            //    .IsUnicode(false);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.BF_MKTUNIT)
            //    .HasPrecision(14, 4);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.PER_UNIT)
            //    .HasPrecision(14, 4);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.PER_FEE)
            //    .HasPrecision(14, 2);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.STAFF_UNI)
            //    .HasPrecision(14, 4);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.STAFF_AMT)
            //    .HasPrecision(14, 2);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.STAFF_FEE)
            //    .HasPrecision(14, 2);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.MKT_PAY)
            //    .HasPrecision(14, 4);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.OLD_MKTUNIT)
            //    .HasPrecision(14, 4);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.OLD_MKTPAY)
            //    .HasPrecision(20, 8);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.MKT_UNIY1)
            //    .HasPrecision(14, 4);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.MKT_UNIY2)
            //    .HasPrecision(14, 4);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.MKT_UNIY3)
            //    .HasPrecision(14, 4);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.LEVEL1_PAY)
            //    .HasPrecision(20, 8);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.LEVEL2_PAY)
            //    .HasPrecision(20, 8);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.LEVEL3_PAY)
            //    .HasPrecision(20, 8);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.LEVEL1_PAY2)
            //    .HasPrecision(20, 8);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.LEVEL2_PAY2)
            //    .HasPrecision(20, 8);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.LEVEL3_PAY2)
            //    .HasPrecision(20, 8);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.LEVEL1_PAY3)
            //    .HasPrecision(20, 8);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.LEVEL2_PAY3)
            //    .HasPrecision(20, 8);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.LEVEL3_PAY3)
            //    .HasPrecision(20, 8);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.MKT_UNI_NEW)
            //    .HasPrecision(14, 4);

            //modelBuilder.Entity<OPNMFEEMKT>()
            //    .Property(e => e.MKT_FEE_NEW)
            //    .HasPrecision(14, 2);
            //#endregion

            #region OPNMFEEAGENT
            modelBuilder.Entity<OPNMFEEAGENT>()
                    .Property(e => e.FUND)
                    .IsUnicode(false);

            modelBuilder.Entity<OPNMFEEAGENT>()
                .Property(e => e.AGENT)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMFEEAGENT>()
                .Property(e => e.FEE_AMT)
                .HasPrecision(14, 2);

            modelBuilder.Entity<OPNMFEEAGENT>()
                .Property(e => e.FEE_EXT)
                .HasPrecision(14, 2);

            modelBuilder.Entity<OPNMFEEAGENT>()
                .Property(e => e.NET_AMT)
                .HasPrecision(16, 4);

            modelBuilder.Entity<OPNMFEEAGENT>()
                .Property(e => e.TOT_UNI)
                .HasPrecision(16, 4);

            modelBuilder.Entity<OPNMFEEAGENT>()
                .Property(e => e.AGENT_UNI)
                .HasPrecision(14, 4);

            modelBuilder.Entity<OPNMFEEAGENT>()
                .Property(e => e.AGENT_FEE)
                .HasPrecision(14, 2);

            modelBuilder.Entity<OPNMFEEAGENT>()
                .Property(e => e.UPD_BY)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMFEEAGENT>()
                .Property(e => e.BF_AGENTUNIT)
                .HasPrecision(14, 4);

            modelBuilder.Entity<OPNMFEEAGENT>()
                .Property(e => e.PER_UNIT)
                .HasPrecision(14, 4);

            modelBuilder.Entity<OPNMFEEAGENT>()
                .Property(e => e.PER_FEE)
                .HasPrecision(14, 2);

            modelBuilder.Entity<OPNMFEEAGENT>()
                .Property(e => e.AGENT_UNI_NEW)
                .HasPrecision(14, 4);

            modelBuilder.Entity<OPNMFEEAGENT>()
                .Property(e => e.AGENT_FEE_NEW)
                .HasPrecision(14, 2);
            #endregion

        }

        public int SaveChanges(bool isWithLog)
        {
            LOG_SYSTEM logSystem = new LOG_SYSTEM();
            logSystem.ACTION_TYPE = 1; // todo แก้
            logSystem.USERNAME = ApplicationManager.UserName; // todo แก้

            LogAdded(logSystem);
            LogModified(logSystem);
            LogDeleted(logSystem);

            this.LOG_SYSTEM.Add(logSystem);

            return base.SaveChanges();
        }

        void LogAdded(LOG_SYSTEM logSystem)
        {

            var addEntities = ChangeTracker.Entries()
             .Where(p => p.State == EntityState.Added).ToList();

            foreach (var change in addEntities)
            {
                LOG_ROW logRow = logSystem.NewRow();
                logRow.STATUS = change.State.ToString();
                logRow.TABLE_NAME = change.Entity.GetType().Name;

                var primaryKey = GetColumnKeyName(change);

                foreach (var prop in change.CurrentValues.PropertyNames)
                {
                    LOG_COLUMN logColumn = logRow.NewColumn();
                    logColumn.COLUMN_NAME = prop;
                    logColumn.IS_KEY = primaryKey.Contains(prop);
                    if (change.CurrentValues[prop] != null)
                    {
                        logColumn.NEW_VALUE = change.CurrentValues[prop].ToString();
                    }
                }
            }
        }

        void LogModified(LOG_SYSTEM logSystem)
        {
            var modifiedEntities = ChangeTracker.Entries()
             .Where(p => p.State == EntityState.Modified).ToList();

            foreach (var change in modifiedEntities)
            {
                LOG_ROW logRow = logSystem.NewRow();
                logRow.STATUS = change.State.ToString();
                logRow.TABLE_NAME = change.Entity.GetType().Name;

                var primaryKey = GetColumnKeyName(change);

                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    var iskey = primaryKey.Contains(prop);

                    var originalValue = change.OriginalValues[prop] != null ? change.OriginalValues[prop].ToString() : "";
                    var currentValue = change.CurrentValues[prop] != null ? change.CurrentValues[prop].ToString() : "";
                    if (originalValue != currentValue | iskey)
                    {
                        LOG_COLUMN logColumn = logRow.NewColumn();
                        logColumn.COLUMN_NAME = prop;
                        logColumn.IS_KEY = primaryKey.Contains(prop);
                        logColumn.OLD_VALUE = originalValue;
                        logColumn.NEW_VALUE = currentValue;

                    }
                }
            }
        }

        void LogDeleted(LOG_SYSTEM logSystem)
        {

            var addEntities = ChangeTracker.Entries()
             .Where(p => p.State == EntityState.Deleted).ToList();

            foreach (var change in addEntities)
            {
                LOG_ROW logRow = logSystem.NewRow();
                logRow.STATUS = change.State.ToString();
                logRow.TABLE_NAME = change.Entity.GetType().Name;

                var primaryKey = GetColumnKeyName(change);

                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    LOG_COLUMN logColumn = logRow.NewColumn();
                    logColumn.COLUMN_NAME = prop;
                    logColumn.IS_KEY = primaryKey.Contains(prop);
                    if (change.OriginalValues[prop] != null)
                    {
                        logColumn.OLD_VALUE = change.OriginalValues[prop].ToString();
                    }
                }
            }
        }

        private string[] GetColumnKeyName(DbEntityEntry entry)
        {
            System.Data.Entity.Core.Metadata.Edm.EntitySetBase setBase = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity).EntitySet;

            string[] keyNames = setBase.ElementType.KeyMembers.Select(k => k.Name).ToArray();
            return keyNames;
        }

        private void GenScript()
        {
            var script = (this as IObjectContextAdapter).ObjectContext.CreateDatabaseScript();
            System.IO.File.WriteAllText(@"D:\Code Job\TFundSolution\TFundSolution.Services\GenScript.sql", script);
        }

    }


}
