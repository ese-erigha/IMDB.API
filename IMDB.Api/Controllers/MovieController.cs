using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IMDB.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResponseDto = IMDB.Api.Models.ResponseDto;
using ViewModel = IMDB.Api.Models.ViewModel;


namespace IMDB.Api.Controllers
{
    [Route("api/movies")]
    public class MovieController : GenericController<ResponseDto.Movie, Entities.Movie>
    {
        readonly IMapper _mapper;
        readonly ICacheService _cacheService;
        readonly IMovieService _service;
        readonly IMovieGenreService _movieGenreService;
        readonly IMoviePersonService _moviePersonService;
        readonly IMoviePlotKeywordService _moviePlotKeywordService;

        public MovieController(IMapper mapper, ICacheService cacheService, IMovieService service, IMovieGenreService movieGenreService, IMoviePersonService moviePersonService, IMoviePlotKeywordService moviePlotKeywordService) :base(mapper, cacheService, service)
        {
            _mapper = mapper;
            _cacheService = cacheService;
            _service = service;
            _movieGenreService = movieGenreService;
            _moviePersonService = moviePersonService;
            _moviePlotKeywordService = moviePlotKeywordService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ViewModel.Movie viewModel) 
        {
            Entities.Movie movieEntity = _mapper.Map<Entities.Movie>(viewModel);

            foreach(long genre in viewModel.Genres)
            {
                var movieGenre = new Entities.MovieGenre { Movie = movieEntity, GenreId = genre };
                _movieGenreService.Create(movieGenre);
            }

            foreach(long keyword in viewModel.PlotKeywords)
            {
                var moviePlotKeyword = new Entities.MoviePlotKeyword { Movie = movieEntity, PlotKeywordId = keyword };
                _moviePlotKeywordService.Create(moviePlotKeyword);
            }


            foreach(long actor in viewModel.Actors)
            {
                var moviePerson = new Entities.MoviePerson { Movie = movieEntity, PersonId = actor };
                _moviePersonService.Create(moviePerson);
            }

            var state = await _moviePersonService.Commit();

            if (!state)
            {
                return StatusCode(500, "A problem occurred while handling your request");
            }

            var modelToReturn = _mapper.Map<ResponseDto.Movie>(movieEntity);

            return CreatedAtRoute(
                //routeName: "GetSingleModel",
                routeValues: new { id = modelToReturn.Id },
                value: modelToReturn
            );

        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetSingle(long id)
        {
            var entity = await _service.GetAll()
                                       .Include(m => m.MovieGenres)
                                            .ThenInclude(mg => mg.Genre)
                                        .Include(m => m.MoviePlotKeywords)
                                            .ThenInclude(mp => mp.PlotKeyword)
                                        .Include(m => m.MoviePersons)
                                            .ThenInclude(mp => mp.Person)
                                        .Include(m => m.Director)
                                        .Include(m => m.Country)
                                        .Include(m => m.ContentRating)
                                        .Include(m => m.Language)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<ResponseDto.Movie>(entity);
            return Ok(model);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ViewModel.Movie viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var entity = await _service.GetAll()
                                       .Include(m => m.MovieGenres)
                                       .Include(m => m.MoviePersons)
                                       .Include(m => m.MoviePlotKeywords)
                                       .AsTracking()
                                       .FirstOrDefaultAsync(m => m.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            _mapper.Map(viewModel, entity); //Updates Entity with ViewModel attributes


            //Movie Genres
            List<Entities.MovieGenre> movieGenresToDelete = entity.MovieGenres.Where(mg => viewModel.Genres.All(genreId => genreId != mg.GenreId)).ToList();
            foreach( var mg in movieGenresToDelete)
            {
                entity.MovieGenres.Remove(mg);
            }

            List<long> genreIdsToAdd = viewModel.Genres.Where(genreId => entity.MovieGenres.All(mg => mg.GenreId != genreId)).ToList();

            //Update MovieGenres
            foreach(long Id in genreIdsToAdd)
            {
                entity.MovieGenres.Add(new Entities.MovieGenre { MovieId = entity.Id, GenreId = Id });
            }


            //Movie Persons
            List<Entities.MoviePerson> moviePersonsToDelete = entity.MoviePersons.Where(mp => viewModel.Actors.All(actorId => actorId != mp.PersonId)).ToList();
            foreach (var mg in moviePersonsToDelete)
            {
                entity.MoviePersons.Remove(mg);
            }

            List<long> actorIdsToAdd = viewModel.Actors.Where(actorId => entity.MoviePersons.All(mp => mp.PersonId != actorId)).ToList();

            //Update MoviePersons
            foreach (long Id in actorIdsToAdd)
            {
                entity.MoviePersons.Add(new Entities.MoviePerson { MovieId = entity.Id, PersonId = Id });
            }

            //Movie PlotKeywords
            List<Entities.MoviePlotKeyword> moviePlotKeywordToDelete = entity.MoviePlotKeywords.Where(mp => viewModel.PlotKeywords.All(plotKeywordId => plotKeywordId != mp.PlotKeywordId)).ToList();
            foreach (var mg in moviePlotKeywordToDelete)
            {
                entity.MoviePlotKeywords.Remove(mg);
            }

            List<long> plotKeywordIdsToAdd = viewModel.PlotKeywords.Where(plotKeywordId => entity.MoviePlotKeywords.All(mp => mp.PlotKeywordId != plotKeywordId)).ToList();

            //Update MoviePlotKeywords
            foreach (long Id in plotKeywordIdsToAdd)
            {
                entity.MoviePlotKeywords.Add(new Entities.MoviePlotKeyword { MovieId = entity.Id, PlotKeywordId = Id });
            }


            //return Ok(entity);
            _service.Update(entity);
            var state = await _service.Commit();
            return !state ? StatusCode(500, "A problem occurred while handling your request") : (IActionResult)NoContent();
        }


        //Delete a record
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(long id)
        {
            return await Task.Run(()=> StatusCode(StatusCodes.Status405MethodNotAllowed));
        }
    }
}
