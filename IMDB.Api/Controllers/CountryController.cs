using System.Threading.Tasks;
using AutoMapper;
using IMDB.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ResponseDto = IMDB.Api.Models.ResponseDto;
using ViewModel = IMDB.Api.Models.ViewModel;

namespace IMDB.Api.Controllers
{
    [Route("api/countries")]
    public class CountryController : GenericController<ResponseDto.Country, Entities.Country>
    {
        readonly IMapper _mapper;
        readonly ICacheService _cacheService;
        readonly ICountryService _service;

        public CountryController(IMapper mapper, ICacheService cacheService, ICountryService service) : base(mapper, cacheService, service)
        {
            _mapper = mapper;
            _cacheService = cacheService;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ViewModel.Country viewModel)
        {
            return await base.Create(viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ViewModel.Country viewModel)
        {
            return await base.Update(id, viewModel);
        }

    }
}
