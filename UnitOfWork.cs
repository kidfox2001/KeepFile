using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestModel;
using TestService;

namespace TestService
{

    public interface IUnitOfWork : IDisposable
    {

        IGenericRepository<OPNCONTL> OPNCONTL_Repo { get; }

        void Save();
        void SaveWithLog();

    }

    public class UnitOfWorkDummy : IUnitOfWork
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

    public class UnitOfWorkTest : IUnitOfWork
    {

        private ModelCsi context = new ModelCsi();

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

        private GenericRepository<TMP_TEST> _TMP_TEST;
        public IGenericRepository<TMP_TEST> TMP_TEST
        {
            get
            {
                if (this._TMP_TEST == null)
                {
                    this._TMP_TEST = new GenericRepository<TMP_TEST>(context);
                }
                return this._TMP_TEST;
            }
        }

        private GenericRepository<TEST_EF_MAIN> _TEST_EF_MAIN;
        public IGenericRepository<TEST_EF_MAIN> TEST_EF_MAIN_Repo
        {
            get
            {
                if (this._TEST_EF_MAIN == null)
                {
                    this._TEST_EF_MAIN = new GenericRepository<TEST_EF_MAIN>(context);
                }
                return this._TEST_EF_MAIN;
            }
        }


        private GenericRepository<TEST_EF_DESCRIPTION> _TEST_EF_DESCRIPTION;
        public IGenericRepository<TEST_EF_DESCRIPTION> TEST_EF_DESCRIPTION_Repo
        {
            get
            {
                if (this._TEST_EF_DESCRIPTION == null)
                {
                    this._TEST_EF_DESCRIPTION = new GenericRepository<TEST_EF_DESCRIPTION>(context);
                }
                return this._TEST_EF_DESCRIPTION;
            }
        }

        private GenericRepository<LOG_MAIN> _LOG_MAIN;
        public IGenericRepository<LOG_MAIN> LOG_MAIN_Repo
        {
            get
            {
                if (this._LOG_MAIN == null)
                {
                    this._LOG_MAIN = new GenericRepository<LOG_MAIN>(context);
                }
                return this._LOG_MAIN;
            }
        }


        private GenericRepository<LOG_TABLE> _LOG_TABLE;
        public IGenericRepository<LOG_TABLE> LOG_TABLE_Repo
        {
            get
            {
                if (this._LOG_TABLE == null)
                {
                    this._LOG_TABLE = new GenericRepository<LOG_TABLE>(context);
                }
                return this._LOG_TABLE;
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
}
