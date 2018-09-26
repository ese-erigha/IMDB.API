using System;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Api.Models.ViewModel
{
    public class ContentRating : BaseModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
    }
}
