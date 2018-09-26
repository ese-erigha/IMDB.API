using System.ComponentModel.DataAnnotations;
namespace IMDB.Api.Models.ViewModel
{
    public class AuthUser : BaseModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
