using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Api.Entities
{
    public class Genre : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public IList<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
