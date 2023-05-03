using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeRentApp.Data;
using OfficeRentApp.DTO;
using OfficeRentApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OfficeRentApp.Repositories.UserAuthRepositories
{
    public class UserAuth:IUserAuth
    {
        private readonly OfficeRentDbContext _context;
        private IConfiguration _config;
        public UserAuth(OfficeRentDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public string RegisterUser(UserRegister registerRequest)
        {
            if (_context.Users.Any(x => x.UserName == registerRequest.UserName)) 
                return "Account with this username is already registered";

            if (_context.Users.Any(x => x.Email == registerRequest.Email))
                return "Account with this email is already registered";

            User newUser = new User();
            newUser.UserName = registerRequest.UserName;
            newUser.PasswordHash = CreatePasswordHash(registerRequest.Password);
            newUser.Email = registerRequest.Email;
            newUser.FirstName = registerRequest.FirstName;
            newUser.LastName = registerRequest.LastName;
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return "User registered succesfully!";
        }

        public UserDto Login([FromBody] UserLogin userLogin)
        {
            var userDto = Authenticate(userLogin);
            if (userDto != null)
            {
                userDto.Token = Generate(userDto);
                return userDto;
            }
            return null;
        }

        private string Generate(UserDto userDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, _context.Roles.Where(x => x.Id == userDto.RoleId).Select(r => r.Role).First()),
                new Claim(ClaimTypes.Name, userDto.UserName),
                new Claim(ClaimTypes.GivenName, userDto.FirstName),
                new Claim(ClaimTypes.Surname, userDto.LastName)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserDto Authenticate(UserLogin userLogin)
        {
            byte[] userLoginPasswordHash = CreatePasswordHash(userLogin.Password);

            var userDto = _context.Users.Include(u => u.Role)
                .Where(cx => cx.Email == userLogin.Email && cx.PasswordHash == userLoginPasswordHash)
                .Select(o => new UserDto
                {
                    Id = o.Id,
                    UserName = o.UserName,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    RoleId = o.RoleId,
                    Token = o.Token
                }).First();

            if (userDto != null)
            {
                return userDto;
            }
            return null;
        }

        private byte[] CreatePasswordHash(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] passwordHash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                return passwordHash;
            }
        }
    }
}
