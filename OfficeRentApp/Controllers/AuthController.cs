using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using OfficeRentApp.DTO;
using OfficeRentApp.Repositories.UserAuthRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OfficeRentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController:ControllerBase
    {
        private readonly IUserAuth _userauth;
        public AuthController(IUserAuth userAuth)
        {
            _userauth = userAuth;
        }
        
        [HttpPost("register")]
        public string Register(UserRegister request)
        {
            return _userauth.RegisterUser(request);
        }

        [HttpPost("login")]
        public string Login(UserLogin login)
        {
           return _userauth.Login(login);
        }
    }
}
