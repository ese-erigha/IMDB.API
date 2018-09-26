using System;
using IMDB.Api.Core;
using IMDB.Api.Repositories.Interfaces;

namespace IMDB.Api.Repositories.Implementations
{
	public class MoviePlotkeywordRepository : GenericRepository<Entities.MoviePlotKeyword>, IMoviePlotKeywordRepository
    {
        public MoviePlotkeywordRepository(DatabaseContext context): base(context)
        {
        }
    }
}
