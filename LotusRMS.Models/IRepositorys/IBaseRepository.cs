using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IBaseRepository<T> where T:class
    {
        T? Get(int id);
        T? GetByGuid(Guid id);
        Task<T?> GetByGuidAsync(Guid id);
        ICollection<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,IOrderedQueryable<T>> orderBy=null,
            string includeProperties = null
            );
        Task<ICollection<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,IOrderedQueryable<T>> orderBy=null,
            string includeProperties = null
            );
        T? GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );
        T? GetLastOrDefault(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );
        Task<T?> GetFirstOrDefaultAsync(
           Expression<Func<T, bool>> filter = null,
           string includeProperties = null
           );
        void Add(T entity);
        Task AddAsync(T entity);
        void Remove(int id);
        Task RemoveAsync(int id);
        void RemoveRange(IEnumerable<T> entity);
        void Remove(T entity);
        Task RemoveAsync(T entity);
        Task<ICollection<T>> FindBy(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        IQueryable<T> GetQueryable();
        void Save();
        Task SaveAsync();
        Task<bool> HasAnyAsync(Expression<Func<T, bool>> filter = null);


    }
}
