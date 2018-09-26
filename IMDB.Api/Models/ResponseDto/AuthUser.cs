using System;
namespace IMDB.Api.Models.ResponseDto
{
    public class AuthUser : User
    {
        public string RefreshToken { get; set; }

        public string AccessToken { get; set; }
    }
}
