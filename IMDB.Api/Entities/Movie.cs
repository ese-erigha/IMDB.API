using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Api.Entities
{
    public class Movie : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public long Gross { get; set; }

        [Required]
        public long Budget { get; set; }

        [Required]
        public string ImdbLink { get; set; }

        [Required]
        public float ImdbScore { get; set; }

        [Required]
        public float AspectRatio { get; set; }

        [Required]
        public int Year { get; set; }


        public long DirectorId { get; set; }
        public Person Director { get; set; }


        public long CountryId { get; set; }
        public Country Country { get; set; }


        public long LanguageId { get; set; }
        public Language Language { get; set; }


        public long ContentRatingId { get; set; }
        public ContentRating ContentRating { get; set; }


        public IList<MovieGenre> MovieGenres { get; set; } 
        public IList<MoviePlotKeyword> MoviePlotKeywords { get; set; } 
        public IList<MoviePerson> MoviePersons { get; set; } 
    }
}
