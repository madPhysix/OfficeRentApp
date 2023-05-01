using Microsoft.AspNetCore.Mvc;
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
            UserRoleDefine userRole = new UserRoleDefine()
            {
                Id = 0,
                UserId = _context.Users.Where(i => i.UserName == newUser.UserName).Select(x => x.Id).FirstOrDefault(),
                Role = "User"
            };
            _context.Roles.Add(userRole);
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
                new Claim("UserName", userDto.UserName),
                new Claim("FirstName", userDto.FirstName),
                new Claim("LastName", userDto.LastName)
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

            var userDto = _context.Users.Where(cx => cx.Email == userLogin.Email && cx.PasswordHash == userLoginPasswordHash)
                .Join(_context.Roles, cx => cx.Id, rs => rs.UserId, (cx, rs) =>
            new
            {
                Id = cx.Id,
                UserName = cx.UserName,
                Token = cx.Token,
                Role = rs.Role
            });
                         
            if (userDto != null)
            {
                return (UserDto)userDto;
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
