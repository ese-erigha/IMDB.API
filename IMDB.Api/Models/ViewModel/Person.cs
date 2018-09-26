using System;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Api.Models.ViewModel
{
    public class Person : BaseModel
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
    }
}
