using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IMDB.Api.Services.Interfaces;

namespace IMDB.Api.Models.ViewModel
{
    public class User : AuthUser, IValidatableObject
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = (IUserService)validationContext.GetService(typeof(IUserService));
            var validationResults = new List<ValidationResult>();
            validationResults = ValidateEmail(validationResults, service);
            return validationResults;
        }

        List<ValidationResult> ValidateEmail(List<ValidationResult> validationResults, IUserService service)
        {
            var user = service.GetBy(x => x.Email == Email).FirstOrDefault();
            if (user != null)
            {
                validationResults.Add(new ValidationResult("Email already exist", new string[] { "Email" }));
            }

            return validationResults;
        }
    }
}
