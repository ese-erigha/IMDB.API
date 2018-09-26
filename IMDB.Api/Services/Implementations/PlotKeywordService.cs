using System;
using IMDB.Api.Core.Interfaces;
using IMDB.Api.Repositories.Interfaces;
using IMDB.Api.Services.Interfaces;
namespace IMDB.Api.Services.Implementations
{
    public class PlotKeywordService : EntityService<Entities.PlotKeyword>, IPlotKeywordService
    {
        readonly IPlotKeywordRepository _repository;
       

        public PlotKeywordService(IUnitOfWork unitOfWork, IPlotKeywordRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;
           
        }
    }
}