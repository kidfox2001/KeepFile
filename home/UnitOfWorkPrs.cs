using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePrs
{
    public interface IUnitOfWork : IDisposable
    {

        //IGenericRepository<tblItem> tblItemRepository { get; }
        //IGenericRepository<tblItemUnit> tblItemUnitRepository { get; }
        void BulkCopy<T>(IEnumerable<T> source);

        void Save();
        void SaveWithLog();

    }

    public class UnitOfWorkPrs : IUnitOfWork
    {
        private ModelPrs context = new ModelPrs();

        private IGenericRepository<tblItem> _tblItem;
        public IGenericRepository<tblItem> tblItemRepository
        {
            get
            {
                if (this._tblItem == null)
                {
                    this._tblItem = new GenericRepository<tblItem>(context);
                }
                return this._tblItem;
            }
        }

        private IGenericRepository<tblItemUnit> _tblItemUnit;
        public IGenericRepository<tblItemUnit> tblItemUnitRepository
        {
            get
            {
                if (this._tblItemUnit == null)
                {
                    this._tblItemUnit = new GenericRepository<tblItemUnit>(context);
                }
                return this._tblItemUnit;
            }
        }

        private IGenericRepository<tblShip> _tblShip;
        public IGenericRepository<tblShip> tblShipRepository
        {
            get
            {
                if (this._tblShip == null)
                {
                    this._tblShip = new GenericRepository<tblShip>(context);
                }
                return this._tblShip;
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

            //   context.SaveChanges();
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

        public void SaveWithLog()
        {
            throw new NotImplementedException();
        }

        public void BulkCopy<T>(IEnumerable<T> source)
        {
            throw new NotImplementedException();
        }
    }

    public class GenericUnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        public GenericUnitOfWork(ModelPrs context)
        {
            _context = context;
        }

        private Dictionary<Type, Object> repositories = new Dictionary<Type, object>();
        public IGenericRepository<TEntity> Resipotory<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            }

            IGenericRepository<TEntity> resipotory = new GenericRepository<TEntity>(_context);
            repositories.Add(typeof(TEntity), resipotory);
            return resipotory;
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
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

        public void SaveWithLog()
        {
            throw new NotImplementedException();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void BulkCopy<T>(IEnumerable<T> source)
        {
            using (var connection = new SqlConnection("data source=27.254.81.243;initial catalog=PRS;persist security info=True;user id=ter;password=We187;MultipleActiveResultSets=True"))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                {
                    bulkCopy.BatchSize = 100;
                    bulkCopy.DestinationTableName = typeof(T).Name;
                    try
                    {
                        bulkCopy.WriteToServer(source.AsDataTable());
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        
                    }
                }

                connection.Close();
            }

           
        }
    }
}
