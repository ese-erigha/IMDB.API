using System;
namespace IMDB.Api.Entities
{
    public class MovieGenre: BaseEntity
    {
        public long MovieId { get; set; }

        public Movie Movie { get; set; }

        public long GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
