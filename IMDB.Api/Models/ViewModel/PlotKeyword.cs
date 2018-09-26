using System;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Api.Models.ViewModel
{
    public class PlotKeyword : BaseModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
    }
}
