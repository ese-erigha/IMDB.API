using System;
using IMDB.Api.Core;
using IMDB.Api.Repositories.Interfaces;

namespace IMDB.Api.Repositories.Implementations
{
    public class ContentRatingRepository : GenericRepository<Entities.ContentRating>, IContentRatingRepository
    {
        public ContentRatingRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
