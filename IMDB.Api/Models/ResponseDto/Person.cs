using System;


namespace IMDB.Api.Models.ResponseDto
{
    public class Person : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
