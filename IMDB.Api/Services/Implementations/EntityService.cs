using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IMDB.Api.Services.Interfaces;
using IMDB.Api.Core.Interfaces;
using IMDB.Api.Repositories.Interfaces;
using System.Linq;

namespace IMDB.Api.Services.Implementations
{
    public class EntityService<T> : IEntityService<T> where T : Entities.BaseEntity
    {
        readonly IUnitOfWork _unitOfWork;
        IGenericRepository<T> _repository;
        
        public EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public void Create(T entity)
        {
            _repository.Add(entity);
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll()
                              .OrderBy(a => a.UpdatedAt);                
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetBy(predicate)
                              .OrderBy(a => a.UpdatedAt);
        }

        public async Task<T> GetByIdAsync(long Id)
        {
            return await _repository.GetByIdAsync(Id);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        public async Task<bool> Commit()
        {
            return await _unitOfWork.Commit();
        }
    }
}
