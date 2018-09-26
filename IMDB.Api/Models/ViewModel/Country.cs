using System;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Api.Models.ViewModel
{
    public class Country : BaseModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
