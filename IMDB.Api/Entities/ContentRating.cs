using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Api.Entities
{
    public class ContentRating : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public IList<Movie> Movies { get; set; } = new List<Movie>();
    }
}
