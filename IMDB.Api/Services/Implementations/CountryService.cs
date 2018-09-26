using System;
using IMDB.Api.Core.Interfaces;
using IMDB.Api.Repositories.Interfaces;
using IMDB.Api.Services.Interfaces;

namespace IMDB.Api.Services.Implementations
{
    public class CountryService : EntityService<Entities.Country>, ICountryService
    {
        readonly ICountryRepository _repository;
       

        public CountryService(IUnitOfWork unitOfWork, ICountryRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;
        }
    }
}
