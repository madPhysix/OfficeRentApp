using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using OfficeRentApp.DTO;
using OfficeRentApp.Models;
using OfficeRentApp.Repositories.UserAuthRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using OfficeRentApp.Helpers;

namespace OfficeRentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuth _userauth;
        private readonly RandomCodeMaker _randomCodeMaker;
        public AuthController(IUserAuth userAuth, RandomCodeMaker randomCode)
        {
            _userauth = userAuth;
            _randomCodeMaker = randomCode;
        }

        [HttpPost("register")]
        public string Register(UserRegister request)
        {
            return _userauth.RegisterUser(request);
        }

        [HttpPost("login")]
        public Result Login(UserLogin login)
        {
            return _userauth.Login(login);
        }

        [Authorize]
        [HttpPost("Refresh-Token")]
        public Result RefreshToken(UserDto userDto)
        {
            return _userauth.RefreshToken(userDto);
        }

        [Authorize]
        [HttpPost("PasswordChange")]
        public Result PasswordChange(PasswordDto passwordDto)
        {
            string? email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            if (_userauth.PasswordChange(email, passwordDto))
            {
                return new Result { Status = true };
            }
            return new Result { Status = false, Message = "Password is wrong" };
        }

        [HttpPost("RestorePassword")]
        public Result RestorePassword(RestorePasswordDto restorePasswordDto)
        {
            if (!_userauth.RestorePassword(restorePasswordDto).Status)
            {
                return new Result { Status = false, Message = "Password is wrong" };
            }
            return new Result { Status = true };
        }

        [HttpPost("GenerateOTP")]
        public Result GenerateOTP(EmailDto emailDto)
        {
            int code = _randomCodeMaker.CreateRandomCode();
            Result result = _userauth.ForgotPassword(emailDto, code);
            if (result.Status)
            {
                return new Result
                {
                    Data = Tuple.Create(emailDto, code),
                    Status = true
                };
            }
            return result;
        }
    }
}
