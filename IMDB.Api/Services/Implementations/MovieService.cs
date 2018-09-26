using System;
using System.Threading.Tasks;
using IMDB.Api.Core.Interfaces;
using IMDB.Api.Entities;
using IMDB.Api.Repositories.Interfaces;
using IMDB.Api.Services.Interfaces;
namespace IMDB.Api.Services.Implementations
{
    public class MovieService : EntityService<Entities.Movie>, IMovieService
    {
        readonly IMovieRepository _repository;

        public MovieService(IUnitOfWork unitOfWork, IMovieRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;
           
        }

    }
}