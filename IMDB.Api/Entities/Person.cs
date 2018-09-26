using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Api.Entities
{
    public class Person : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public IList<Movie> DirectedMovies { get; set; } = new List<Movie>();

        public IList<MoviePerson> MoviePersons { get; set; } = new List<MoviePerson>();
    }
}
