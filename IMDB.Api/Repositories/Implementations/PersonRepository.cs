using System;
using IMDB.Api.Core;
using IMDB.Api.Repositories.Interfaces;

namespace IMDB.Api.Repositories.Implementations
{
	public class PersonRepository : GenericRepository<Entities.Person>, IPersonRepository
    {
        public PersonRepository(DatabaseContext context): base(context)
        {
        }
    }
}
