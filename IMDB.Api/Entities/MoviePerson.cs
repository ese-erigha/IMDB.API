using System;
namespace IMDB.Api.Entities
{
    public class MoviePerson : BaseEntity
    {
        public long MovieId { get; set; }

        public Movie Movie { get; set; }

        public long PersonId { get; set; }

        public Person Person { get; set; }
    }
}

