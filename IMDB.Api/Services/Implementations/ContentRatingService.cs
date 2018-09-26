using System;
using IMDB.Api.Core.Interfaces;
using IMDB.Api.Repositories.Interfaces;
using IMDB.Api.Services.Interfaces;
using System.Threading.Tasks;

namespace IMDB.Api.Services.Implementations
{
    public class ContentRatingService : EntityService<Entities.ContentRating>, IContentRatingService
    {
        readonly IContentRatingRepository _repository;
       

        public ContentRatingService(IUnitOfWork unitOfWork, IContentRatingRepository repository) :base(unitOfWork,repository)
        {
            _repository = repository;

        }

    }
}
