using IMDB.Api.Core.Interfaces;
using ResponseDto = IMDB.Api.Models.ResponseDto;
using IMDB.Api.Repositories.Interfaces;
using IMDB.Api.Services.Interfaces;
using AutoMapper;
using System.Threading.Tasks;

namespace IMDB.Api.Services.Implementations
{
    public class UserService : EntityService<Entities.User>, IUserService
    {
        readonly IUserRepository _repository;

       
        public UserService(IUnitOfWork unitOfWork, IUserRepository repository, ITokenService tokenService, IMapper mapper) : base(unitOfWork, repository)
        {
            _repository = repository;

        }

    }
}
