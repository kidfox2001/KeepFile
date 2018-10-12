using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TFundSolution.Utils.Objects;


namespace TFundSolution.Services
{

   public interface IGenericRepository<TEntity> where TEntity : class
    {

          IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

          IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

          IEnumerable<TEntity> GetAll();

          TEntity GetByID(object id);
        
          void Insert(TEntity entity);

          void Update(TEntity entityToUpdate);

          void UpdateWithLog(TEntity entityToUpdate);

          void Delete(object id);

          void Delete(TEntity entityToDelete);

          IEnumerable<TEntity> GetByStoredProcedure(string spName, params SqlParameter[] parameters);

    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity :class
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;


        public GenericRepository(DbContext context)
        {
           

            this.context = context;
            this.dbSet = context.Set<TEntity>();

            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            //context.Database.Log = s => (new LogFile()).writeLog(s);
            
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }


        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void UpdateWithLog(TEntity entityToUpdate)
        {
            //TEntity x = entityToUpdate.CloneObject<TEntity>();

            //dbSet.Attach(entityToUpdate);
            //context.Entry(entityToUpdate).Reload();
            //context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual IEnumerable<TEntity> GetByStoredProcedure(string spName, params SqlParameter[] parameters)
        {
            string partPara = string.Join(",", parameters.Select(q => q.ParameterName).ToArray());
            string command = string.Format("exec {0} {1}", spName, partPara);
            return context.Database.SqlQuery<TEntity>(command, parameters).ToList();
        }

    }


    public class DummyGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal List<TEntity> items  = new List<TEntity>();

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {

            IQueryable<TEntity> query = from i in items.AsQueryable() select i ;


            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }

        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = from i in items.AsQueryable() select i;


            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return items;
        }

        public TEntity GetByID(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(TEntity entity)
        {
             items.Add(entity);
        }

        public void Update(TEntity entityToUpdate)
        {
           var item =  items.Where(i => i == entityToUpdate).SingleOrDefault();
           if (item != null)
           {
               items.Remove(item);
               items.Add(entityToUpdate);
           }
        }

        public void UpdateWithLog(TEntity entityToUpdate)
        {
            var item = items.Where(i => i == entityToUpdate).SingleOrDefault();
            if (item != null)
            {
                items.Remove(item);
                items.Add(entityToUpdate);
            }

        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entityToDelete)
        {
            items.Remove(entityToDelete);
        }

        public IEnumerable<TEntity> GetByStoredProcedure(string spName, params SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }


     
    }
}
