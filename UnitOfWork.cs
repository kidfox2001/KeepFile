using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFundSolution.Models;

namespace TFundSolution.Services
{

    public interface IUnitOfWork : IDisposable
    {

        IGenericRepository<OPNMAGEN> OPNMAGEN_Repo { get; }
        IGenericRepository<OPNCONTL> OPNCONTL_Repo { get; }
        IGenericRepository<FEE_SETTING> FEE_SETTING_Repo { get; }
        IGenericRepository<FEE_SETTING_ONGO> FEE_SETTING_ONGO_Repo { get; }

        void Save();
        void SaveWithLog();

    }

    public class UnitOfWorkTFund : IUnitOfWork
    {

        private ModelTFund context = new ModelTFund();

        private GenericRepository<OPNCONTL> _OPNCONTL;
        public IGenericRepository<OPNCONTL> OPNCONTL_Repo
        {
            get
            {
                if (this._OPNCONTL == null)
                {
                    this._OPNCONTL = new GenericRepository<OPNCONTL>(context);
                }
                return this._OPNCONTL;
            }
        }

        private GenericRepository<OPNMAGEN> _OPNMAGEN;
        public IGenericRepository<OPNMAGEN> OPNMAGEN_Repo
        {
            get 
            {
                if (this._OPNMAGEN == null)
                {
                    this._OPNMAGEN = new GenericRepository<OPNMAGEN>(context);
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
                    this._LOG_SYSTEM = new GenericRepository<LOG_SYSTEM>(context);
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
                    this._FEE_SETTING = new GenericRepository<FEE_SETTING>(context);
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
                    this._FEE_SETTING_ONGO = new GenericRepository<FEE_SETTING_ONGO>(context);
                }
                return this._FEE_SETTING_ONGO;
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
                context.SaveChanges();
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
                context.SaveChanges(true);
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

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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

    public class UnitOfWorkDummyTFund : IUnitOfWork
    {

        private DummyGenericRepository<OPNCONTL> _OPNCONTL;
        public IGenericRepository<OPNCONTL> OPNCONTL_Repo
        {
            get
            {
                if (this._OPNCONTL == null)
                {
                    this._OPNCONTL = new DummyGenericRepository<OPNCONTL>();
                }
                return this._OPNCONTL;
            }
        }

        private DummyGenericRepository<OPNMAGEN> _OPNMAGEN;
        public IGenericRepository<OPNMAGEN> OPNMAGEN_Repo
        {
            get
            {
                if (this._OPNMAGEN == null)
                {
                    this._OPNMAGEN = new DummyGenericRepository<OPNMAGEN>();
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

     

        public void Save()
        {
            //throw new NotImplementedException();
        }


        public void SaveWithLog()
        {
            //throw new NotImplementedException();
        }

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

    }


}
