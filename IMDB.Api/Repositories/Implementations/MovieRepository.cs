using System;
using System.Linq;
using System.Threading.Tasks;
using IMDB.Api.Core;
using IMDB.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IMDB.Api.Repositories.Implementations
{
	public class MovieRepository : GenericRepository<Entities.Movie>, IMovieRepository
    {
        public MovieRepository(DatabaseContext context): base(context)
        {
        }

    }
}
