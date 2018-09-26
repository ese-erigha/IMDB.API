using System;
using IMDB.Api.Core.Interfaces;
using IMDB.Api.Repositories.Interfaces;
using IMDB.Api.Services.Interfaces;

namespace IMDB.Api.Services.Implementations
{
    public class MovieGenreService : EntityService<Entities.MovieGenre>, IMovieGenreService
    {
        readonly IMovieGenreRepository _repository;

        public MovieGenreService(IUnitOfWork unitOfWork, IMovieGenreRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;

        }
    }
}
