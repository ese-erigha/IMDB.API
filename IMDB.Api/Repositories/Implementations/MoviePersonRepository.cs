using System;
using IMDB.Api.Core;
using IMDB.Api.Repositories.Interfaces;

namespace IMDB.Api.Repositories.Implementations
{
    public class MoviePersonRepository: GenericRepository<Entities.MoviePerson>, IMoviePersonRepository
    {
        public MoviePersonRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
