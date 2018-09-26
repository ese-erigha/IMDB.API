using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IMDB.Api.Services.Interfaces
{
    public interface IEntityService<T> : IService where T : Entities.BaseEntity
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);

        void Create(T entity);

        Task<T> GetByIdAsync(long Id);

        void Update(T entity);

        void Delete(T entity);

        Task<bool> Commit();
    }
}
