using System;
using ViewModel = IMDB.Api.Models.ViewModel;
using ResponseDto = IMDB.Api.Models.ResponseDto;

namespace IMDB.Api.Services.Interfaces
{
    public interface ITokenService : IService
    {
        string HashPassword(ViewModel.AuthUser user, string password);

        bool VerifyPassword(ViewModel.AuthUser viewModelUser, Entities.User entityUser);

        string CreateAccessToken(Entities.User user);

        string CreateRefreshToken(Entities.User user);

    }
}
