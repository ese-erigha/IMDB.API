using System;
namespace IMDB.Api.Entities
{
    public class MoviePlotKeyword : BaseEntity
    {
        public long PlotKeywordId { get; set; }

        public PlotKeyword PlotKeyword { get; set; }

        public long MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
