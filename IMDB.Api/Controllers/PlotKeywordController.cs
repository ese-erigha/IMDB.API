using System.Threading.Tasks;
using AutoMapper;
using IMDB.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ResponseDto = IMDB.Api.Models.ResponseDto;
using ViewModel = IMDB.Api.Models.ViewModel;

namespace IMDB.Api.Controllers
{
    [Route("api/plot-keywords")]
    public class PlotKeywordController : GenericController<ResponseDto.PlotKeyword, Entities.PlotKeyword>
    {
        readonly IMapper _mapper;
        readonly ICacheService _cacheService;
        readonly IPlotKeywordService _service;

        public PlotKeywordController(IMapper mapper, ICacheService cacheService, IPlotKeywordService service) : base(mapper, cacheService, service)
        {
            _mapper = mapper;
            _cacheService = cacheService;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ViewModel.PlotKeyword viewModel)
        {
            return await base.Create(viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ViewModel.PlotKeyword viewModel)
        {
            return await base.Update(id, viewModel);
        }

    }
}
