using System;
using System.Collections.Generic;

namespace IMDB.Api.Models.ResponseDto
{
    public class Movie : BaseModel
    {

        public string Title { get; set; }

        public int Duration { get; set; }

        public long Gross { get; set; }

        public long Budget { get; set; }

        public string ImdbLink { get; set; }

        public float ImdbScore { get; set; }

        public float AspectRatio { get; set; }

        public int Year { get; set; }

        public Person Director { get; set; }

        public Country Country { get; set; }

        public ContentRating ContentRating { get; set; }

        public Language Language { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public IEnumerable<PlotKeyword> PlotKeywords { get; set; }

        public IEnumerable<Person> Cast { get; set; }
    }
}
