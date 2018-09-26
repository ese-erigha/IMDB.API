using System;
using IMDB.Api.Core.Interfaces;
using IMDB.Api.Repositories.Interfaces;
using IMDB.Api.Services.Interfaces;

namespace IMDB.Api.Services.Implementations
{
    public class MoviePlotKeywordService : EntityService<Entities.MoviePlotKeyword>, IMoviePlotKeywordService
    {
        readonly IMoviePlotKeywordRepository _repository;

        public MoviePlotKeywordService(IUnitOfWork unitOfWork, IMoviePlotKeywordRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;
        }
    }
}
