using System.Threading.Tasks;
using AutoMapper;
using IMDB.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ResponseDto = IMDB.Api.Models.ResponseDto;
using ViewModel = IMDB.Api.Models.ViewModel;

namespace IMDB.Api.Controllers
{
    [Route("api/content-ratings")]
    public class ContentRatingController : GenericController<ResponseDto.ContentRating,Entities.ContentRating>
    {
        readonly IMapper _mapper;
        readonly ICacheService _cacheService;
        readonly IContentRatingService _service;

        public ContentRatingController(IMapper mapper, ICacheService cacheService, IContentRatingService service) : base(mapper,cacheService,service)
        {
            _mapper = mapper;
            _cacheService = cacheService;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ViewModel.ContentRating viewModel)
        {
            return await base.Create(viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id,[FromBody] ViewModel.ContentRating viewModel)
        {
            return await base.Update(id,viewModel);
        }

    }
}
