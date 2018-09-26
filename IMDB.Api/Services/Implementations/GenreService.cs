using System;
using IMDB.Api.Core.Interfaces;
using IMDB.Api.Repositories.Interfaces;
using IMDB.Api.Services.Interfaces;
namespace IMDB.Api.Services.Implementations
{
    public class GenreService : EntityService<Entities.Genre>, IGenreService
    {
        readonly IGenreRepository _repository;

        public GenreService(IUnitOfWork unitOfWork, IGenreRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;
        }
    }
}
