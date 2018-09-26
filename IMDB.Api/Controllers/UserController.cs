using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IMDB.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResponseDto = IMDB.Api.Models.ResponseDto;
using ViewModel = IMDB.Api.Models.ViewModel;

namespace IMDB.Api.Controllers
{   
    //[Authorize]
    [Route("api/users")]
    public class UserController : GenericController<ResponseDto.User, Entities.User>
    {
        readonly IMapper _mapper;
        readonly ICacheService _cacheService;
        readonly IUserService _service;

        public UserController(IMapper mapper, ICacheService cacheService, IUserService service) : base(mapper, cacheService, service)
        {
            _mapper = mapper;
            _cacheService = cacheService;
            _service = service;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ViewModel.User viewModel)
        {
            return await base.Update(id, viewModel);
        }

    }
}
