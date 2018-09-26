using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IMDB.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel = IMDB.Api.Models.ViewModel;
using ResponseDto = IMDB.Api.Models.ResponseDto;
using Microsoft.Extensions.Options;
using IMDB.Api.Helpers;
using System;

namespace IMDB.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        readonly IUserService _userService;
        readonly ICacheService _cacheService;
        readonly ITokenService _tokenService;
        readonly IMapper _mapper;
        readonly AppSettings _appSettings;

        public AuthController(IUserService userService, ICacheService cacheService, ITokenService tokenService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _cacheService = cacheService;
            _tokenService = tokenService;
            _mapper = mapper;
            _appSettings = appSettings.Value;

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ViewModel.User user)
        {
            user.Password = _tokenService.HashPassword(user, user.Password);
            

            var entity = _mapper.Map<Entities.User>(user);
            _userService.Create(entity);
            var state = await _userService.Commit();

            if (!state)
            {
                return StatusCode(500, "A problem occurred while handling your request");
            }

            var modelToReturn = _mapper.Map<ResponseDto.User>(entity);
            return Ok(modelToReturn);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ViewModel.AuthUser viewModelUser)
        {
            var entityUser = _userService.GetBy(x => x.Email == viewModelUser.Email).FirstOrDefault();
            if (entityUser == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            if (!_tokenService.VerifyPassword(viewModelUser, entityUser))
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            ResponseDto.AuthUser authUser = _mapper.Map<ResponseDto.AuthUser>(entityUser);
            authUser.AccessToken = _tokenService.CreateAccessToken(entityUser);
            var refreshToken = _tokenService.CreateRefreshToken(entityUser);
            authUser.RefreshToken = refreshToken;
            entityUser.RefreshToken = refreshToken;

            _userService.Update(entityUser);
            var state = await _userService.Commit();
            if(!state){
                return StatusCode(500, "A problem occurred while handling your request");
            }
            return Ok(authUser);
        }


        [AllowAnonymous]
        [HttpGet("refresh-token/{token}")]
        public IActionResult RefreshToken(string token)
        {
            var entityUser = _userService.GetBy(x => x.RefreshToken == token).FirstOrDefault();
            if (entityUser == null)
            {
                return BadRequest(new { message = "Invalid Token" });
            }

            ResponseDto.AuthUser authUser = _mapper.Map<ResponseDto.AuthUser>(entityUser);
            authUser.AccessToken = _tokenService.CreateAccessToken(entityUser);
            return Ok(authUser);
        }

    }
}
