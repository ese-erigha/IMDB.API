using System;
using IMDB.Api.Core.Interfaces;
using IMDB.Api.Repositories.Interfaces;
using IMDB.Api.Services.Interfaces;
namespace IMDB.Api.Services.Implementations
{
    public class PersonService : EntityService<Entities.Person>, IPersonService
    {
        readonly IPersonRepository _repository;
      
        public PersonService(IUnitOfWork unitOfWork, IPersonRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;
        }
    }
}