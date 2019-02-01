using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using TFundSolution.Utils.Objects;
using TFundSolution.Utils.EntityFramworks;
using TFundSolution.Models;

namespace TFundSolution.Services
{

    public interface IGenericUnitOfWork : IDisposable
    {


        IGenericRepository<TEntity> RepositoryCis<TEntity>() where TEntity : class;
        IGenericRepository<TEntity> ResipotoryOper<TEntity>() where TEntity : class;
        IGenericRepository<TEntity> ResipotorySas<TEntity>() where TEntity : class;


        void Save();
        void SaveWithLog();
        //void SaveBulk();
        void BulkCopyAndSave<T>(IEnumerable<T> source,bool isDispose = false, int? batchSize = null);
        void ReNewContext();
        /// <summary>
        /// ยกเลิกการใช้ lazy load แต่ จะโดนหมดทุก context ที่มีการใช้ context ร่วมกัน
        /// </summary>
        void SetNotLazy();
        int ExecuteSql(string cmd, IEnumerable<OracleParameter> para);
        IEnumerable<T> SqlQuery<T>(string cmd, IEnumerable<OracleParameter> para);
        ModelTFundCis GetContextCis();

    }

    public class GenericUnitOfWork : IGenericUnitOfWork
    {

        public ModelTFundCis ContextCis;
        public ModelTFundOper ContextOper;
        public ModelTFundSas ContextSas;

        public GenericUnitOfWork(ModelTFundCis contextCis, ModelTFundOper contextOper, ModelTFundSas contextSas)
        {
            ContextCis = contextCis;
            ContextOper = contextOper;
            ContextSas = contextSas;
        }


        public ModelTFundCis GetContextCis()
        {
            return ContextCis;
        }

        private Dictionary<Type, Object> repositories = new Dictionary<Type, object>();
        public IGenericRepository<TEntity> RepositoryCis<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            }

            IGenericRepository<TEntity> resipotory = new GenericRepository<TEntity>(ContextCis);
            repositories.Add(typeof(TEntity), resipotory);
            return resipotory;
        }
        public IGenericRepository<TEntity> ResipotoryOper<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            }

            IGenericRepository<TEntity> resipotory = new GenericRepository<TEntity>(ContextOper);
            repositories.Add(typeof(TEntity), resipotory);
            return resipotory;
        }
        public IGenericRepository<TEntity> ResipotorySas<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            }

            IGenericRepository<TEntity> resipotory = new GenericRepository<TEntity>(ContextSas);
            repositories.Add(typeof(TEntity), resipotory);
            return resipotory;
        }

        public void Save()
        {
            try
            {
                ContextCis.SaveChanges();
                //contextOper.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                            ve.PropertyName,
                                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                            ve.ErrorMessage);
                    }
                }
                throw;
            }

            //   context.SaveChanges();
        }

        //public void SaveBulk()
        //{
        //    try
        //    {
        //        ContextCis.BulkSaveChanges();
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
        //                                    ve.PropertyName,
        //                                    eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
        //                                    ve.ErrorMessage);
        //            }
        //        }
        //        throw;
        //    }
        //}

        public void SaveWithLog()
        {
            throw new NotImplementedException();
        }

        public int ExecuteSql(string cmd, IEnumerable<OracleParameter> para)
        {
            return ContextCis.Database.ExecuteSqlCommand(cmd, para.ToArray());
        }

        public IEnumerable<T> SqlQuery<T>(string cmd, IEnumerable<OracleParameter> para)
        {
            return ContextCis.Database.SqlQuery<T>(cmd, para.ToArray()).ToList();
        }

        public void BulkCopyAndSave<T>(IEnumerable<T> source,bool isDispose = false, int? batchSize = null)
        {
            var mapping = TFundSolution.Utils.EntityFramworks.EfMappingFactory.GetMappingsForContext(this.ContextCis);
            var currentType = typeof(T);
            var typeMapping = mapping.TypeMappings[typeof(T)];
            var tableMapping = typeMapping.TableMappings.First();

            IList<ColumnMapping> properties = tableMapping.PropertyMappings
                .Where(p => currentType.IsSubclassOf(p.ForEntityType) || p.ForEntityType == currentType)
                .Select(p => new ColumnMapping { NameInDatabase = p.ColumnName, NameOnObject = p.PropertyName }).ToList();
            if (tableMapping.TPHConfiguration != null)
            {
                properties.Add(new ColumnMapping
                {
                    NameInDatabase = tableMapping.TPHConfiguration.ColumnName,
                    StaticValue = tableMapping.TPHConfiguration.Mappings[typeof(T)]
                });
            }

            using (var reader = new EFDataReader<T>(source, properties))
            {
                var connection = new Oracle.DataAccess.Client.OracleConnection(ContextCis.Database.Connection.ConnectionString);
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
                using (Oracle.DataAccess.Client.OracleBulkCopy copy = new Oracle.DataAccess.Client.OracleBulkCopy(connection, Oracle.DataAccess.Client.OracleBulkCopyOptions.UseInternalTransaction))
                {
                    copy.BatchSize = Math.Min(reader.RecordsAffected, batchSize ?? 10000); //default batch size
                    copy.BulkCopyTimeout = 3600;
                    if (!string.IsNullOrWhiteSpace(tableMapping.Schema))
                    {
                        copy.DestinationTableName = string.Format("{0}.{1}", tableMapping.Schema, tableMapping.TableName);
                    }
                    else
                    {
                        copy.DestinationTableName = tableMapping.TableName;
                    }

                    copy.NotifyAfter = 0;

                    foreach (var i in Enumerable.Range(0, reader.FieldCount))
                    {
                        copy.ColumnMappings.Add(i, properties[i].NameInDatabase);
                    }

                    var sourceTable = source.AsDataTableWithContext(this.ContextCis);
                    copy.WriteToServer(sourceTable);
                    copy.Close();
                }

                if (isDispose)
                {
                    this.ReNewContext();    
                }
                
            }
        }

        public void ReNewContext()
        {
            ContextCis.Dispose();
            ContextOper.Dispose();
            ContextSas.Dispose();

            ContextCis = new ModelTFundCis();
            ContextOper = new ModelTFundOper();
            ContextSas = new ModelTFundSas();
        }

        public void SetNotLazy()
        {
            this.ContextCis.Configuration.ProxyCreationEnabled = false;
            this.ContextOper.Configuration.ProxyCreationEnabled = false;
            this.ContextSas.Configuration.ProxyCreationEnabled = false;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ContextCis.Dispose();
                    ContextOper.Dispose();
                    ContextSas.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



    }

    public interface IUnitOfWork : IDisposable
    {

        IGenericRepository<Agent> Agent_Repo { get; }
        IGenericRepository<Fund> Fund_Repo { get; }
        IGenericRepository<FEE_SETTING> FEE_SETTING_Repo { get; }
        IGenericRepository<FEE_SETTING_ONGO> FEE_SETTING_ONGO_Repo { get; }
        IGenericRepository<FEE_SETTING_TBANK> FEE_SETTING_TBANK_Repo { get; }
        IGenericRepository<FEE_SETTING_ONPAY> FEE_SETTING_ONPAY_Repo { get; }
        IGenericRepository<FEE_SETTING_YEAR> FEE_SETTING_YEAR_Repo { get; }
        IGenericRepository<FEE_SETTING_ONLEVEL> FEE_SETTING_ONLEVEL_Repo { get; }
        IGenericRepository<FEE_SETTING_UPFRONT> FEE_SETTING_UPFRONT_Repo { get; }
        IGenericRepository<FEE_SETTING_UPFRONT_ONLEVEL> FEE_UPFRONT_ONLEVEL_Repo { get; }
        IGenericRepository<FeeAgentDaily> FeeAgentDaily_Repo { get; }
        IGenericRepository<OPRTNAV> OPRTNAV_Repo { get; }
        IGenericRepository<V_CALENDAR> V_CALENDAR_Repo { get; }
        IGenericRepository<DailyNav> V_W_NAV_Repo { get; }

        void Save();
        void SaveWithLog();
       // void SaveBulk();
        int ExecuteSql(string cmd, IEnumerable<OracleParameter> para);

    }

    public class UnitOfWorkTFund : IUnitOfWork
    {
        public ModelTFundCis ContextCis;
        public ModelTFundOper ContextOper;

        public UnitOfWorkTFund(ModelTFundCis contextCis, ModelTFundOper contextOper)
        {
            this.ContextCis = (ModelTFundCis)contextCis;
            this.ContextOper = (ModelTFundOper)contextOper;
        }

        private GenericRepository<Fund> _OPNCONTL;
        public IGenericRepository<Fund> Fund_Repo
        {
            get
            {
                if (this._OPNCONTL == null)
                {
                    this._OPNCONTL = new GenericRepository<Fund>(ContextCis);
                }
                return this._OPNCONTL;
            }
        }

        private GenericRepository<Agent> _OPNMAGEN;
        public IGenericRepository<Agent> Agent_Repo
        {
            get
            {
                if (this._OPNMAGEN == null)
                {
                    this._OPNMAGEN = new GenericRepository<Agent>(ContextCis);
                }
                return this._OPNMAGEN;
            }
        }

        private GenericRepository<LOG_SYSTEM> _LOG_SYSTEM;
        public IGenericRepository<LOG_SYSTEM> LOG_SYSTEM_Repo
        {
            get
            {
                if (this._LOG_SYSTEM == null)
                {
                    this._LOG_SYSTEM = new GenericRepository<LOG_SYSTEM>(ContextCis);
                }
                return this._LOG_SYSTEM;
            }
        }

        private GenericRepository<FEE_SETTING> _FEE_SETTING;
        public IGenericRepository<FEE_SETTING> FEE_SETTING_Repo
        {
            get
            {
                if (this._FEE_SETTING == null)
                {
                    this._FEE_SETTING = new GenericRepository<FEE_SETTING>(ContextCis);
                }
                return this._FEE_SETTING;
            }
        }

        private GenericRepository<FEE_SETTING_ONGO> _FEE_SETTING_ONGO;
        public IGenericRepository<FEE_SETTING_ONGO> FEE_SETTING_ONGO_Repo
        {
            get
            {
                if (this._FEE_SETTING_ONGO == null)
                {
                    this._FEE_SETTING_ONGO = new GenericRepository<FEE_SETTING_ONGO>(ContextCis);
                }
                return this._FEE_SETTING_ONGO;
            }
        }

        private GenericRepository<FEE_SETTING_TBANK> _FEE_SETTING_TBANK;
        public IGenericRepository<FEE_SETTING_TBANK> FEE_SETTING_TBANK_Repo
        {
            get
            {
                if (this._FEE_SETTING_TBANK == null)
                {
                    this._FEE_SETTING_TBANK = new GenericRepository<FEE_SETTING_TBANK>(ContextCis);
                }
                return this._FEE_SETTING_TBANK;
            }
        }

        private GenericRepository<FEE_SETTING_ONPAY> _FEE_SETTING_ONPAY;
        public IGenericRepository<FEE_SETTING_ONPAY> FEE_SETTING_ONPAY_Repo
        {
            get
            {
                if (this._FEE_SETTING_ONPAY == null)
                {
                    this._FEE_SETTING_ONPAY = new GenericRepository<FEE_SETTING_ONPAY>(ContextCis);
                }
                return this._FEE_SETTING_ONPAY;
            }
        }

        private GenericRepository<FEE_SETTING_YEAR> _FEE_SETTING_YEAR;
        public IGenericRepository<FEE_SETTING_YEAR> FEE_SETTING_YEAR_Repo
        {
            get
            {
                if (this._FEE_SETTING_YEAR == null)
                {
                    this._FEE_SETTING_YEAR = new GenericRepository<FEE_SETTING_YEAR>(ContextCis);
                }
                return this._FEE_SETTING_YEAR;
            }
        }

        private GenericRepository<FEE_SETTING_ONLEVEL> _FEE_SETTING_ONLEVEL;
        public IGenericRepository<FEE_SETTING_ONLEVEL> FEE_SETTING_ONLEVEL_Repo
        {
            get
            {
                if (this._FEE_SETTING_ONLEVEL == null)
                {
                    this._FEE_SETTING_ONLEVEL = new GenericRepository<FEE_SETTING_ONLEVEL>(ContextCis);
                }
                return this._FEE_SETTING_ONLEVEL;
            }
        }


        private GenericRepository<FEE_SETTING_UPFRONT> _FEE_SETTING_UPFRONT;
        public IGenericRepository<FEE_SETTING_UPFRONT> FEE_SETTING_UPFRONT_Repo
        {
            get
            {
                if (this._FEE_SETTING_UPFRONT == null)
                {
                    this._FEE_SETTING_UPFRONT = new GenericRepository<FEE_SETTING_UPFRONT>(ContextCis);
                }
                return this._FEE_SETTING_UPFRONT;
            }
        }

        private GenericRepository<FEE_SETTING_UPFRONT_ONLEVEL> _FEE_UPFRONT_ONLEVEL;
        public IGenericRepository<FEE_SETTING_UPFRONT_ONLEVEL> FEE_UPFRONT_ONLEVEL_Repo
        {
            get
            {
                if (this._FEE_UPFRONT_ONLEVEL == null)
                {
                    this._FEE_UPFRONT_ONLEVEL = new GenericRepository<FEE_SETTING_UPFRONT_ONLEVEL>(ContextCis);
                }
                return this._FEE_UPFRONT_ONLEVEL;
            }
        }

        private GenericRepository<FeeAgentDaily> _FeeAgentDaily;
        public IGenericRepository<FeeAgentDaily> FeeAgentDaily_Repo
        {
            get
            {
                if (this._FeeAgentDaily == null)
                {
                    this._FeeAgentDaily = new GenericRepository<FeeAgentDaily>(ContextCis);
                }
                return this._FeeAgentDaily;
            }
        }

        private GenericRepository<OPRTNAV> _OPRTNAV_Repo;
        public IGenericRepository<OPRTNAV> OPRTNAV_Repo
        {
            get
            {
                if (this._OPRTNAV_Repo == null)
                {
                    this._OPRTNAV_Repo = new GenericRepository<OPRTNAV>(ContextOper);
                }
                return this._OPRTNAV_Repo;
            }
        }

        private GenericRepository<V_CALENDAR> _V_CALENDAR_Repo;
        public IGenericRepository<V_CALENDAR> V_CALENDAR_Repo
        {
            get
            {
                if (this._V_CALENDAR_Repo == null)
                {
                    this._V_CALENDAR_Repo = new GenericRepository<V_CALENDAR>(ContextOper);
                }
                return this._V_CALENDAR_Repo;
            }

        }

        private GenericRepository<DailyNav> _V_W_NAV_Repo_Repo;
        public IGenericRepository<DailyNav> V_W_NAV_Repo
        {
            get
            {
                if (this._V_W_NAV_Repo_Repo == null)
                {
                    this._V_W_NAV_Repo_Repo = new GenericRepository<DailyNav>(ContextOper);
                }
                return this._V_W_NAV_Repo_Repo;
            }
        }


        //public GenericRepository<T> getGeneric<T>() where T : class
        //{
        //    var repo = new GenericRepository<T>(context);
        //    return repo;
        //}


        public void Save()
        {
            try
            {
                ContextCis.SaveChanges();
                ContextOper.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                            ve.PropertyName,
                                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void SaveWithLog()
        {
            try
            {
                ContextCis.SaveChanges(true);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                            ve.PropertyName,
                                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                            ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        //public void SaveBulk()
        //{
        //    try
        //    {
        //        ContextCis.BulkSaveChanges();
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
        //                                    ve.PropertyName,
        //                                    eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
        //                                    ve.ErrorMessage);
        //            }
        //        }
        //        throw;
        //    }

        //}

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ContextCis.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int ExecuteSql(string cmd, IEnumerable<OracleParameter> para)
        {

            //contextCis.FeeAgentDaily.Where(m => DbFunctions.TruncateTime(m.FEE_DATE) == DbFunctions.TruncateTime(new DateTime(2018,10,31))).DeleteFromQuery();
            return 1;
        }

    }

    public class UnitOfWorkDummyTFund : IUnitOfWork
    {

        private DummyGenericRepository<Fund> _OPNCONTL;
        public IGenericRepository<Fund> Fund_Repo
        {
            get
            {
                if (this._OPNCONTL == null)
                {
                    this._OPNCONTL = new DummyGenericRepository<Fund>();
                }
                return this._OPNCONTL;
            }
        }

        private DummyGenericRepository<Agent> _OPNMAGEN;
        public IGenericRepository<Agent> Agent_Repo
        {
            get
            {
                if (this._OPNMAGEN == null)
                {
                    this._OPNMAGEN = new DummyGenericRepository<Agent>();
                }
                return this._OPNMAGEN;
            }
        }

        private DummyGenericRepository<FEE_SETTING> _FEE_SETTING;
        public IGenericRepository<FEE_SETTING> FEE_SETTING_Repo
        {
            get
            {
                if (this._FEE_SETTING == null)
                {
                    this._FEE_SETTING = new DummyGenericRepository<FEE_SETTING>();
                }
                return this._FEE_SETTING;
            }
        }

        private DummyGenericRepository<FEE_SETTING_ONGO> _FEE_SETTING_ONGO;
        public IGenericRepository<FEE_SETTING_ONGO> FEE_SETTING_ONGO_Repo
        {
            get
            {
                if (this._FEE_SETTING_ONGO == null)
                {
                    this._FEE_SETTING_ONGO = new DummyGenericRepository<FEE_SETTING_ONGO>();
                }
                return this._FEE_SETTING_ONGO;
            }
        }

        private DummyGenericRepository<FEE_SETTING_TBANK> _FEE_SETTING_TBANK_Repo;
        public IGenericRepository<FEE_SETTING_TBANK> FEE_SETTING_TBANK_Repo
        {
            get
            {
                if (this._FEE_SETTING_TBANK_Repo == null)
                {
                    this._FEE_SETTING_TBANK_Repo = new DummyGenericRepository<FEE_SETTING_TBANK>();
                }
                return this._FEE_SETTING_TBANK_Repo;
            }
        }

        private DummyGenericRepository<FEE_SETTING_ONPAY> _FEE_SETTING_ONPAY_Repo;
        public IGenericRepository<FEE_SETTING_ONPAY> FEE_SETTING_ONPAY_Repo
        {
            get
            {
                if (this._FEE_SETTING_ONPAY_Repo == null)
                {
                    this._FEE_SETTING_ONPAY_Repo = new DummyGenericRepository<FEE_SETTING_ONPAY>();
                }
                return this._FEE_SETTING_ONPAY_Repo;
            }
        }

        private DummyGenericRepository<FEE_SETTING_YEAR> _FEE_SETTING_YEAR_Repo;
        public IGenericRepository<FEE_SETTING_YEAR> FEE_SETTING_YEAR_Repo
        {
            get
            {
                if (this._FEE_SETTING_YEAR_Repo == null)
                {
                    this._FEE_SETTING_YEAR_Repo = new DummyGenericRepository<FEE_SETTING_YEAR>();
                }
                return this._FEE_SETTING_YEAR_Repo;
            }
        }

        private DummyGenericRepository<FEE_SETTING_ONLEVEL> _FEE_SETTING_ONLEVEL_Repo;
        public IGenericRepository<FEE_SETTING_ONLEVEL> FEE_SETTING_ONLEVEL_Repo
        {
            get
            {
                if (this._FEE_SETTING_ONLEVEL_Repo == null)
                {
                    this._FEE_SETTING_ONLEVEL_Repo = new DummyGenericRepository<FEE_SETTING_ONLEVEL>();
                }
                return this._FEE_SETTING_ONLEVEL_Repo;
            }
        }

        private DummyGenericRepository<FEE_SETTING_UPFRONT> _FEE_SETTING_UPFRONT_Repo;
        public IGenericRepository<FEE_SETTING_UPFRONT> FEE_SETTING_UPFRONT_Repo
        {
            get
            {
                if (this._FEE_SETTING_UPFRONT_Repo == null)
                {
                    this._FEE_SETTING_UPFRONT_Repo = new DummyGenericRepository<FEE_SETTING_UPFRONT>();
                }
                return this._FEE_SETTING_UPFRONT_Repo;
            }
        }

        private DummyGenericRepository<FEE_SETTING_UPFRONT_ONLEVEL> _FEE_UPFRONT_ONLEVEL_Repo;
        public IGenericRepository<FEE_SETTING_UPFRONT_ONLEVEL> FEE_UPFRONT_ONLEVEL_Repo
        {
            get
            {
                if (this._FEE_UPFRONT_ONLEVEL_Repo == null)
                {
                    this._FEE_UPFRONT_ONLEVEL_Repo = new DummyGenericRepository<FEE_SETTING_UPFRONT_ONLEVEL>();
                }
                return this._FEE_UPFRONT_ONLEVEL_Repo;
            }
        }

        private DummyGenericRepository<OPRTNAV> _OPRTNAV_Repo;
        public IGenericRepository<OPRTNAV> OPRTNAV_Repo
        {
            get
            {
                if (this._OPRTNAV_Repo == null)
                {
                    this._OPRTNAV_Repo = new DummyGenericRepository<OPRTNAV>();
                }
                return this._OPRTNAV_Repo;
            }
        }

        private DummyGenericRepository<V_CALENDAR> _V_CALENDAR_Repo;
        public IGenericRepository<V_CALENDAR> V_CALENDAR_Repo
        {
            get
            {
                if (this._V_CALENDAR_Repo == null)
                {
                    this._V_CALENDAR_Repo = new DummyGenericRepository<V_CALENDAR>();
                }
                return this._V_CALENDAR_Repo;
            }

        }

        private DummyGenericRepository<DailyNav> _V_W_NAV_Repo_Repo;
        public IGenericRepository<DailyNav> V_W_NAV_Repo
        {
            get
            {
                if (this._V_W_NAV_Repo_Repo == null)
                {
                    this._V_W_NAV_Repo_Repo = new DummyGenericRepository<DailyNav>();
                }
                return this._V_W_NAV_Repo_Repo;
            }
        }

        public void Save()
        {
            //throw new NotImplementedException();
        }

        //public void SaveBulk()
        //{
        //    //throw new NotImplementedException();
        //}

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveWithLog()
        {
            throw new NotImplementedException();
        }


        public int ExecuteSql(string cmd, IEnumerable<OracleParameter> para)
        {
            throw new NotImplementedException();
        }


        public IGenericRepository<FeeAgentDaily> FeeAgentDaily_Repo
        {
            get { throw new NotImplementedException(); }
        }
    }




}
