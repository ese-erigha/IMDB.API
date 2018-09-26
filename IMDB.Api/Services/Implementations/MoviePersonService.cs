using System;
using IMDB.Api.Core.Interfaces;
using IMDB.Api.Repositories.Interfaces;
using IMDB.Api.Services.Interfaces;

namespace IMDB.Api.Services.Implementations
{
    public class MoviePersonService : EntityService<Entities.MoviePerson>, IMoviePersonService
    {
        readonly IMoviePersonRepository _repository;

        public MoviePersonService(IUnitOfWork unitOfWork, IMoviePersonRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;

        }
    }
}
