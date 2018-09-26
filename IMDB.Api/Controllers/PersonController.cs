using System.Threading.Tasks;
using AutoMapper;
using IMDB.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ResponseDto = IMDB.Api.Models.ResponseDto;
using ViewModel = IMDB.Api.Models.ViewModel;

namespace IMDB.Api.Controllers
{
    [Route("api/persons")]
    public class PersonController : GenericController<ResponseDto.Person, Entities.Person>
    {
        readonly IMapper _mapper;
        readonly ICacheService _cacheService;
        readonly IPersonService _service;

        public PersonController(IMapper mapper, ICacheService cacheService, IPersonService service) : base(mapper, cacheService, service)
        {
            _mapper = mapper;
            _cacheService = cacheService;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ViewModel.Person viewModel)
        {
            return await base.Create(viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ViewModel.Person viewModel)
        {
            return await base.Update(id, viewModel);
        }


        //Delete a record
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(long id)
        {
            return await Task.Run(() => StatusCode(StatusCodes.Status405MethodNotAllowed));
        }
    }

}
