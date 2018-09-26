using System;
using IMDB.Api.Core;
using IMDB.Api.Repositories.Interfaces;

namespace IMDB.Api.Repositories.Implementations
{
	public class LanguageRepository : GenericRepository<Entities.Language>, ILanguageRepository
    {
        public LanguageRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
