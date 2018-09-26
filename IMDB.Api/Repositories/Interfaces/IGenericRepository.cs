using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace IMDB.Api.Repositories.Interfaces
{
    public interface IGenericRepository <T> where T : Entities.BaseEntity
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(long Id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

    }
}
