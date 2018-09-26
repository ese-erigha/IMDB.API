using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Api.Entities
{
    public class Language : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public IList<Movie> Movies { get; set; } = new List<Movie>();
    }
}
     