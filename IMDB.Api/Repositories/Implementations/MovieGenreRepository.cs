using System;
using IMDB.Api.Core;
using IMDB.Api.Repositories.Interfaces;

namespace IMDB.Api.Repositories.Implementations
{
    public class MovieGenreRepository : GenericRepository<Entities.MovieGenre>, IMovieGenreRepository
    {
        public MovieGenreRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
  