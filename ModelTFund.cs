using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data.Entity.Infrastructure;
using TFundSolution.Models;

namespace TFundSolution.Services
{

    // validate
    // authen
    // update เก็บ log ผิด
    // tracking
    // todo วันที่ไม่ให้พิมพ์
    // ทำให้สวย

    public partial class ModelTFund : DbContext
    {
        public ModelTFund()
            : base("name=ModelTFund")
        {
            Database.SetInitializer<ModelTFund>(null);
           // GenScript();
        }

        public virtual DbSet<OPNMAGEN> OPNMAGEN { get; set; }
        public virtual DbSet<OPNCONTL> OPNCONTL { get; set; }
        public virtual DbSet<LOG_SYSTEM> LOG_SYSTEM { get; set; }
        public virtual DbSet<FEE_SETTING> FEE_SETTING { get; set; }
        public virtual DbSet<FEE_SETTING_ONGO> FEE_SETTING_ONGO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            #region OPNCONTL

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.FUND)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.HOLDER)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.CISNO)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.CERNO)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.NAV)
                .HasPrecision(7, 4);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.FLG_NEW)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.FLG_PRN)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.FUND_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.FUND_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.MIN_SHARE)
                .HasPrecision(14, 4);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.FLG_CER)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.DOC_NO)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.BANK_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.BANK_ADDR)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.BANK_TEL)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.MOR_NO)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.SIGN_AMT)
                .HasPrecision(14, 2);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.BUY_MIN_SHARE)
                .HasPrecision(14, 4);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.SELL_MIN_SHARE)
                .HasPrecision(14, 4);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.BUY_NEXT)
                .HasPrecision(14, 4);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.SWITCH_OUT)
                .HasPrecision(14, 4);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.SWITCH_IN)
                .HasPrecision(14, 4);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.NAV_BUY)
                .HasPrecision(7, 4);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.NAV_SELL)
                .HasPrecision(7, 4);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.UPD_BY)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.SELL_MIN_AMT)
                .HasPrecision(14, 2);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.BUY_NEXT_BATH)
                .HasPrecision(14, 2);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.MIN_BATH)
                .HasPrecision(14, 2);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.ORDER_SB)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.ORDER_RD)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.CLASS_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.TYPE_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.MATCH_NO)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.AUDIT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.AUDIT_COMP)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.FUND_COND)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.FUND_POLICY)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.FUND_RETURN)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.FEE_MANAGEMENT)
                .HasPrecision(5, 2);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.FEE_TRUSTEE)
                .HasPrecision(5, 2);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.TAX_ID)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.REDM_FREQ)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.REDM_DAY_OF_WEEK)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.START_DAY_OF_MONTH)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.END_DAY_OF_MONTH)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.TRADE_START_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.TRADE_END_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.DIVIDENT_FREQ)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.BUY_FREQ)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.BUY_DAY_OF_WEEK)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.BSTART_DAY_OF_MONTH)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.BEND_DAY_OF_MONTH)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.BF_AMOUNT)
                .HasPrecision(14, 2);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.MATURITY)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.MERCHANT_ID)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.AR_MIN_BATH)
                .HasPrecision(7, 2);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.SEND_WEB)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.AR_FLG)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.IVR_FLG)
                .IsUnicode(false);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.AR_PERCENT)
                .HasPrecision(3, 2);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.SW_NEXT_BATH)
                .HasPrecision(14, 2);

            modelBuilder.Entity<OPNCONTL>()
                .Property(e => e.SW_NEXT_SHARE)
                .HasPrecision(14, 4); 
            #endregion

            #region OPNMAGEN
            modelBuilder.Entity<OPNMAGEN>()
                    .Property(e => e.BR_CODE)
                    .IsUnicode(false);

            modelBuilder.Entity<OPNMAGEN>()
                .Property(e => e.BR_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMAGEN>()
                .Property(e => e.BR_ADDR1)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMAGEN>()
                .Property(e => e.BR_ADDR2)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMAGEN>()
                .Property(e => e.BR_ADDR3)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMAGEN>()
                .Property(e => e.BR_ZIP)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMAGEN>()
                .Property(e => e.BR_PROV)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMAGEN>()
                .Property(e => e.NEW_ORDER)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMAGEN>()
                .Property(e => e.NEW_CISNO)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMAGEN>()
                .Property(e => e.GROUP_FUND)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMAGEN>()
                .Property(e => e.NEW_ORDER_IPO)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMAGEN>()
                .Property(e => e.REGIS_NO)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMAGEN>()
                .Property(e => e.REGIS_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<OPNMAGEN>()
                .Property(e => e.BR_FLAG)
                .IsUnicode(false);
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
                logRow.TABLE_NAME = change.Entity.GetType().BaseType.Name;

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
