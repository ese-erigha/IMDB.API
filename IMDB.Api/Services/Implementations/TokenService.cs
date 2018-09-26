using System;
using System.Security.Cryptography;
using System.Text;
using ViewModel = IMDB.Api.Models.ViewModel;
using IMDB.Api.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using AutoMapper;
using IMDB.Api.Helpers;
using Microsoft.Extensions.Options;

namespace IMDB.Api.Services.Implementations
{
    public class TokenService : ITokenService
    {
        readonly AppSettings _appSettings;
       
        public TokenService(IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _appSettings = appSettings.Value;
           
        }

        public string CreateAccessToken(Entities.User user)
        {

            var now = DateTime.UtcNow;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64),
                }),
                Expires = now.AddMinutes(Double.Parse(_appSettings.AccessTokenExpireMinutes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken =  tokenHandler.WriteToken(token);
            return accessToken;
        }

        public string CreateRefreshToken(Entities.User user)
        {
            var now = DateTime.UtcNow;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64),
                }),
                Expires = now.AddDays(Double.Parse(_appSettings.RefreshTokenExpireDays)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            return accessToken;
        }

        public string HashPassword(ViewModel.AuthUser user, string password)
        {
            var hashedBytes = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(password));
            return Encoding.ASCII.GetString(hashedBytes);
        }

        public bool VerifyPassword(ViewModel.AuthUser viewModelUser, Entities.User entityUser)
        {
            viewModelUser.Password = HashPassword(viewModelUser, viewModelUser.Password);
            return viewModelUser.Password == entityUser.Password;
        }
    }
}
