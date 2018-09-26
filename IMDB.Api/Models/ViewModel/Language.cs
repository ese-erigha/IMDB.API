using System;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Api.Models.ViewModel
{
    public class Language : BaseModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
