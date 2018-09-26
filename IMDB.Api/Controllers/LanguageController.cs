using System.Threading.Tasks;
using AutoMapper;
using IMDB.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ResponseDto = IMDB.Api.Models.ResponseDto;
using ViewModel = IMDB.Api.Models.ViewModel;

namespace IMDB.Api.Controllers
{
    [Route("api/languages")]
    public class LanguagController : GenericController<ResponseDto.Language, Entities.Language>
    {
        readonly IMapper _mapper;
        readonly ICacheService _cacheService;
        readonly ILanguageService _service;

        public LanguagController(IMapper mapper, ICacheService cacheService, ILanguageService service) : base(mapper, cacheService, service)
        {
            _mapper = mapper;
            _cacheService = cacheService;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ViewModel.Language viewModel)
        {
            return await base.Create(viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ViewModel.Language viewModel)
        {
            return await base.Update(id, viewModel);
        }

    }
}
