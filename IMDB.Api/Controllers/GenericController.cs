using System.Threading.Tasks;
using AutoMapper;
using IMDB.Api.Filters;
using IMDB.Api.Helpers;
using IMDB.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ViewModel = IMDB.Api.Models.ViewModel;
using ResponseDto = IMDB.Api.Models.ResponseDto;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IMDB.Api.Controllers
{
    [Route("api/[controller]")]
    [ValidateModel(Disable = false)]
    public class GenericController<R, E> : Controller where R : ResponseDto.BaseModel where E : Entities.BaseEntity
    {
        readonly IMapper _mapper;
        readonly ICacheService _cacheService;
        readonly IEntityService<E> _service;

        protected GenericController(IMapper mapper, ICacheService cacheService, IEntityService<E> service)
        {
            _mapper = mapper;
            _cacheService = cacheService;
            _service = service;
        }

        //Get All Records
        [HttpGet]
        public IActionResult GetAll([FromQuery]PaginationInfo paginationInfo)
        {
            PagedList<R> result;
            var cacheKey = GetType() + paginationInfo.PageNumber.ToString();
            string cachedResponse = _cacheService.Get(cacheKey);

            if (cachedResponse != null)
            {
                result = JsonConvert.DeserializeObject<PagedList<R>>(cachedResponse);
            }
            else
            {
                result = Paginate(_service.GetAll(), paginationInfo);
                _cacheService.Set(cacheKey, result);
            }
            return Ok(result);
        }

        //Get Record
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetSingle(long id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<R>(entity);
            return Ok(model);
        }


        //Create Record
        public async Task<IActionResult> Create<V>(V viewModel) where V : ViewModel.BaseModel
        {
            var entity = _mapper.Map<E>(viewModel);
            _service.Create(entity);
            var state = await _service.Commit();

            if (!state)
            {
                return StatusCode(500, "A problem occurred while handling your request");
            }

            var modelToReturn = _mapper.Map<R>(entity);

            return CreatedAtRoute(
                //routeName: "GetSingleModel",
                routeValues: new { id = entity.Id },
                value: modelToReturn
            );
        }

        //Update a Record
        public async Task<IActionResult> Update<V>(long id, V viewModel) where V : ViewModel.BaseModel
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {

                return NotFound();
            }
            _mapper.Map<V, E>(viewModel, entity);
            entity.Id = id;
            _service.Update(entity);
            var state = await _service.Commit();
            return !state ? StatusCode(500, "A problem occurred while handling your request") : (IActionResult)NoContent();
        }

        //Delete a record
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(long id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            _service.Delete(entity);
            var state = await _service.Commit();
            return !state ? StatusCode(500, "A problem occurred while handling your request") : Accepted();
        }


        PagedList<R> Paginate(IQueryable<E> source, PaginationInfo paginationInfo)
        {
            var count = source.Count();
            IEnumerable<E> items = new List<E>();

            if (count > 0)
            {
                items = source.Skip((paginationInfo.PageNumber - 1) * paginationInfo.PageSize)
                              .Take(paginationInfo.PageSize)
                              .AsEnumerable();
            }

            var mappedItems = _mapper.Map<IEnumerable<R>>(items);
            return new PagedList<R>(mappedItems, count, paginationInfo.PageNumber, paginationInfo.PageSize);
        }
    }
}
