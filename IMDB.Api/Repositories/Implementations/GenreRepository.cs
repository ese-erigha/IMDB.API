using System;
using IMDB.Api.Core;
using IMDB.Api.Repositories.Interfaces;

namespace IMDB.Api.Repositories.Implementations
{
	public class GenreRepository : GenericRepository<Entities.Genre>, IGenreRepository
    {
        public GenreRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
