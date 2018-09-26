using System;
using IMDB.Api.Core;
using IMDB.Api.Repositories.Interfaces;

namespace IMDB.Api.Repositories.Implementations
{
	public class PlotKeywordRepository : GenericRepository<Entities.PlotKeyword>, IPlotKeywordRepository
    {
        public PlotKeywordRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
