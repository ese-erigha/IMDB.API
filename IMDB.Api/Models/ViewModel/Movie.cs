using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using IMDB.Api.Services.Interfaces;

namespace IMDB.Api.Models.ViewModel
{
    public class Movie : BaseModel, IValidatableObject
    {

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Gross is required")]
        public long Gross { get; set; }

        [Required(ErrorMessage = "Budget is required")]
        public long Budget { get; set; }

        [Required(ErrorMessage = "IMDB link is required")]
        public string ImdbLink { get; set; }

        [Required(ErrorMessage = "IMDB score is required")]
        public float ImdbScore { get; set; }

        [Required(ErrorMessage = "Aspect ratio is required")]
        public float AspectRatio { get; set; }

        [Required(ErrorMessage = "Year is required")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Content Rating is required")]
        public long ContentRating { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public long Country { get; set; }

        [Required(ErrorMessage = "Language is required")]
        public long Language { get; set; }

        [Required(ErrorMessage = "Genres are required")]
        public IList<long> Genres { get; set; }

        [Required(ErrorMessage = "PlotKeywords are required")]
        public IList<long> PlotKeywords { get; set; }

        [Required(ErrorMessage = "Actors are required")]
        public IList<long> Actors  { get; set; }

        [Required(ErrorMessage = "Director is required")]
        public long Director  { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            validationResults = ValidateOneToManyCardinality(validationResults, validationContext);

            validationResults = ValidateManyToManyCardinality(validationResults, validationContext);
           
            return validationResults;
        }

        List<ValidationResult> ValidateManyToManyCardinality(List<ValidationResult> validationResults, ValidationContext validationContext)
        {
            //Validate Genres
            var genreService = (IGenreService)validationContext.GetService(typeof(IGenreService));
            foreach(long propId in Genres)
            {
                validationResults = ValidateManyToManyCardinalityForeignKeyProperties(validationResults, genreService, x => x.Id == propId, "Genres",propId);
            }

            //Validate PlotKeywords
            var plotKeywordService = (IPlotKeywordService)validationContext.GetService(typeof(IPlotKeywordService));
            foreach (long propId in PlotKeywords)
            {
                validationResults = ValidateManyToManyCardinalityForeignKeyProperties(validationResults, plotKeywordService, x => x.Id == propId, "PlotKeywords", propId);
            }

            //Validate Actors
            var personService = (IPersonService)validationContext.GetService(typeof(IPersonService));
            foreach (long propId in Actors)
            {
                validationResults = ValidateManyToManyCardinalityForeignKeyProperties(validationResults, personService, x => x.Id == propId, "Actors", propId);
            }
            return validationResults;
        }

        List<ValidationResult> ValidateOneToManyCardinality(List<ValidationResult> validationResults, ValidationContext validationContext)
        {
            //Validate Country
            var countryService = (ICountryService)validationContext.GetService(typeof(ICountryService));
            validationResults = ValidateOneToManyCardinalityForeignKeyProperties(validationResults, countryService, x => x.Id == Country, "Country");


            //Validate Language
            var languageService = (ILanguageService)validationContext.GetService(typeof(ILanguageService));
            validationResults = ValidateOneToManyCardinalityForeignKeyProperties(validationResults, languageService, x => x.Id == Language, "Language");


            //Validate Content Rating
            var contentRatingService = (IContentRatingService)validationContext.GetService(typeof(IContentRatingService));
            validationResults = ValidateOneToManyCardinalityForeignKeyProperties(validationResults, contentRatingService, x => x.Id == ContentRating, "ContentRating");


            //Validate Director
            var personService = (IPersonService)validationContext.GetService(typeof(IPersonService));
            validationResults = ValidateOneToManyCardinalityForeignKeyProperties(validationResults, personService, x => x.Id == Director, "Director");

            return validationResults;
        }

        List<ValidationResult> ValidateManyToManyCardinalityForeignKeyProperties<T>(List<ValidationResult> validationResults, IEntityService<T> service, Expression<Func<T, bool>> predicate, string property,long propId) where T : Entities.BaseEntity
        {
            var entity = service.GetBy(predicate).FirstOrDefault();
            if (entity == null)
            {
                validationResults.Add(new ValidationResult(property.Remove(property.Length-1,1) + " with Id " +propId +" does not exist", new string[] { property }));
            }
            return validationResults;
        }
        List<ValidationResult> ValidateOneToManyCardinalityForeignKeyProperties<T>(List<ValidationResult> validationResults, IEntityService<T> service, Expression<Func<T, bool>> predicate, string property) where T : Entities.BaseEntity
        {
            var entity = service.GetBy(predicate).FirstOrDefault();
            if (entity == null)
            {
                validationResults.Add(new ValidationResult(property + " does not exist", new string[] { property }));
            }
            return validationResults;
        }
    }
}
