using System;
using IMDB.Api.Core;
using IMDB.Api.Repositories.Interfaces;

namespace IMDB.Api.Repositories.Implementations
{
    public class CountryRepository : GenericRepository<Entities.Country>, ICountryRepository
    {
        public CountryRepository(DatabaseContext context): base(context)
        {
        }
    }
}
