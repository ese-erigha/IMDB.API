using System;
using IMDB.Api.Core.Interfaces;
using IMDB.Api.Repositories.Interfaces;
using IMDB.Api.Services.Interfaces;
namespace IMDB.Api.Services.Implementations
{
    public class LanguageService : EntityService<Entities.Language>, ILanguageService
    {
        readonly ILanguageRepository _repository;
       

        public LanguageService(IUnitOfWork unitOfWork, ILanguageRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;

        }
    }
}