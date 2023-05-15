using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeRentApp.Data;
using OfficeRentApp.DTO;
using OfficeRentApp.Models;
using OfficeRentApp.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace OfficeRentApp.Repositories.UserAuthRepositories
{
    public class UserAuth:IUserAuth
    {
        private readonly OfficeRentDbContext _context;
        private IConfiguration _config;
        private EmailSender _emailSender;
        public UserAuth(OfficeRentDbContext context, IConfiguration config, EmailSender emailSender)
        {
            _context = context;
            _config = config;
            _emailSender = emailSender;
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
            newUser.PhoneNumber = registerRequest.PhoneNumber;
            newUser.FirstName = registerRequest.FirstName;
            newUser.LastName = registerRequest.LastName;
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return "User registered succesfully!";
        }

        public Result Login([FromBody] UserLogin userLogin)
        {
            var result = Authenticate(userLogin);
            UserDto userDto = (UserDto)result.Data;
            string message = result.Message;
            if (userDto != null)
            {
                userDto.Token = Generate(userDto);
                return new Result
                {
                    Data = userDto,
                    Status = true
                };
            }
            return new Result
            {
                Status = false,
                Message = message
            };
        }

        public bool PasswordChange(string userMail, PasswordDto passwordDto)
        {
           var user =  _context.Users.First(u => u.Email == userMail);
           
           if(user.PasswordHash.SequenceEqual(CreatePasswordHash(passwordDto.Password)))
            {
                user.PasswordHash = CreatePasswordHash(passwordDto.NewPassword);
                _context.Update(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ForgotPassword(EmailDto emailDto, int sentCode)
        {
            {
                if (!_context.Users.Any(u => u.Email == emailDto.Email))
                    return false;
                    _emailSender.SendEmail(emailDto, sentCode);
                    return true;
                }

            }
        

        public Result RestorePassword(RestorePasswordDto restorePasswordDto) 
        {
            if (restorePasswordDto.Password == restorePasswordDto.ConfirmPassword)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == restorePasswordDto.Email);
                user.PasswordHash = CreatePasswordHash(restorePasswordDto.ConfirmPassword);
                _context.Users.Update(user);
                _context.SaveChanges();
                return new Result { Status = true };
            }
            return new Result { Status = false, Message="Passwords are not same" };
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
                new Claim(ClaimTypes.Surname, userDto.LastName),
                new Claim(ClaimTypes.Email, userDto.Email)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Result Authenticate(UserLogin userLogin)
        {
            byte[] userLoginPasswordHash = CreatePasswordHash(userLogin.Password);
            if (_context.Users.Where(cx => cx.Email == userLogin.Email).Any())
            {
                if (_context.Users.Where(cx => cx.Email == userLogin.Email && cx.PasswordHash == userLoginPasswordHash).Any())
                {
                    var userDto = _context.Users.Include(u => u.Role)
                        .Where(cx => cx.Email == userLogin.Email && cx.PasswordHash == userLoginPasswordHash)
                        .Select(o => new UserDto
                        {
                            Id = o.Id,
                            UserName = o.UserName,
                            FirstName = o.FirstName,
                            LastName = o.LastName,
                            PhoneNumber = o.PhoneNumber,
                            RoleId = o.RoleId,
                            Token = o.Token,
                            Email = o.Email
                        }).FirstOrDefault();
                    return new Result
                    {
                        Status = true,
                        Data = userDto
                    };
                }
                else return new Result
                {
                    Status = false,
                    Message = "Password is wrong"
                };
            }
            return new Result
            {
                Status = false,
                Message = "User not found"
            };
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
