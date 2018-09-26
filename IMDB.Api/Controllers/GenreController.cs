using System.Threading.Tasks;
using AutoMapper;
using IMDB.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ResponseDto = IMDB.Api.Models.ResponseDto;
using ViewModel = IMDB.Api.Models.ViewModel;

namespace IMDB.Api.Controllers
{
    [Route("api/genres")]
    public class GenreController : GenericController<ResponseDto.Genre, Entities.Genre>
    {
        readonly IMapper _mapper;
        readonly ICacheService _cacheService;
        readonly IGenreService _service;

        public GenreController(IMapper mapper, ICacheService cacheService, IGenreService service) : base(mapper, cacheService, service)
        {
            _mapper = mapper;
            _cacheService = cacheService;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ViewModel.Genre viewModel)
        {
            return await base.Create(viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ViewModel.Genre viewModel)
        {
            return await base.Update(id, viewModel);
        }

    }
}
