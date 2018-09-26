using System;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Api.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string RefreshToken { get; set; }
    }
}
