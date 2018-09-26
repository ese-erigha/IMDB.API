using System;
using IMDB.Api.Core;
using IMDB.Api.Repositories.Interfaces;

namespace IMDB.Api.Repositories.Implementations
{
    public class UserRepository : GenericRepository<Entities.User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
