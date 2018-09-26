using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Api.Entities
{
    public class PlotKeyword : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public IList<MoviePlotKeyword> MoviePlotKeywords { get; set; } = new List<MoviePlotKeyword>();
    }
}
