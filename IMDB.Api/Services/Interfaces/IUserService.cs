using System;
using System.Threading.Tasks;
using ResponseDto = IMDB.Api.Models.ResponseDto;
using ViewModel = IMDB.Api.Models.ViewModel;
namespace IMDB.Api.Services.Interfaces
{
    public interface IUserService : IEntityService<Entities.User> 
    {
    }
}
