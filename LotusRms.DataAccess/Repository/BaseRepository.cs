using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Viewmodels.Order;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dal;
        private readonly DbSet<T> dbSet;
        
        protected BaseRepository(ApplicationDbContext dal) {
            _dal = dal;
            this.dbSet = _dal.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
            Save();
        }  
        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity).ConfigureAwait(false);
            await SaveAsync().ConfigureAwait(false);
        }
        public T GetByGuid(Guid id)
        {
            return dbSet.Find(id);
        } 
        public async Task<T?> GetByGuidAsync(Guid id)
        {
            return await dbSet.FindAsync(id).ConfigureAwait(false); ;
        }
        public T? Get(int id)
        {
            return dbSet.Find(id);
        }

        public ICollection<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query=query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach(var includeProperty in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public void Save()
        {
            _dal.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _dal.SaveChangesAsync().ConfigureAwait(false);
        }

        public IQueryable<T> GetQueryable()
        {
            return dbSet;
        }

        public virtual async Task<ICollection<T>> FindBy(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
           var data= await query.ToListAsync().ConfigureAwait(false);
            return data;
        }

        public T? GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
               query= query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.FirstOrDefault();
        }
        public T? GetLastOrDefault(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return orderBy(query).LastOrDefault();
        }
        public async Task<T?> GetLastOrDefaultAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return await orderBy(query).LastOrDefaultAsync().ConfigureAwait(false);
        }
        public void Remove(int id)
        {
            T? entity = dbSet.Find(id);
            Remove(entity);
        }
        public async Task RemoveAsync(int id)
        {
            T? entity = await dbSet.FindAsync(id).ConfigureAwait(false);
           await RemoveAsync(entity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);

        }
        
        
        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);

        }
        

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query =  dbSet;
            if (filter != null)
            {
                query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync().ConfigureAwait(false);
            }
            return await query.ToListAsync().ConfigureAwait(false);
        }
        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<bool> HasAnyAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                return await query.AnyAsync(filter).ConfigureAwait(false);
            }
            return await query.AnyAsync().ConfigureAwait(false);
        }
    }
}
