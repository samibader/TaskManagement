using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(Expression<Func<T, bool>> predicate);
        void Detach(T entity);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        T GetSingleBy(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleByAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        Task<IQueryable<T>> GetAllAsync();
    }

    internal abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties
        protected DbContext db;
        protected readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected DbContext DbContext
        {
            get { return db ?? (db = DbFactory.Init()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Remove(Expression<Func<T, bool>> predicate)
        {
            dbSet.RemoveRange(dbSet.Where(predicate));
        }

        public void Detach(T entity)
        {
            db.Entry(entity).State = EntityState.Detached;
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public T GetSingleBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate).FirstOrDefault();
        }

        public async Task<T> GetSingleByAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet.AsQueryable();
        }

        public Task<T> GetByIdAsync(int id)
        {
            return dbSet.FindAsync(id);
        }

        public Task<IQueryable<T>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }
    }
}
