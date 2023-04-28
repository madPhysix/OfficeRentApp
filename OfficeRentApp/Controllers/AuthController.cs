using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using OfficeRentApp.Data;
using OfficeRentApp.Models.UserModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OfficeRentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly OfficeRentDbContext _context;
        private IConfiguration _config;
        public AuthController(OfficeRentDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult RegisterUser(UserRegister registerRequest)
        {
            User newUser = new User();
            newUser.UserName = registerRequest.UserName;
            newUser.PasswordHash = CreatePasswordHash(registerRequest.Password);
            newUser.Email = registerRequest.Email;
            newUser.Surname = registerRequest.Surname;
            newUser.GivenName = registerRequest.GivenName;
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok("New user successfully registered!");
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);
            if(user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User not found.");
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.GivenName),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.Role)
            };

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(UserLogin userLogin)
        {
            byte[] userLoginPasswordHash = CreatePasswordHash(userLogin.Password);
            var currentUser = _context.Users.FirstOrDefault(x => x.UserName == userLogin.UserName && x.PasswordHash == userLoginPasswordHash);
            if(currentUser != null)
            {
                return currentUser;
            }
            return null;
        }



        private byte[] CreatePasswordHash(string password) 
        { 
            using(MD5 md5 = MD5.Create())
            {
                byte[] passwordHash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                return passwordHash;
            }
        }
    }
}
