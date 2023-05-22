using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeRentApp.DTO;
using OfficeRentApp.Models;
using System.Security.Claims;

namespace OfficeRentApp.Repositories.UserAuthRepositories
{
    public interface IUserAuth
    {

        public string RegisterUser(UserRegister registerRequest);
        public Result Login([FromBody] UserLogin userLogin);
        public bool PasswordChange(string userName, PasswordDto passwordDto);
        public Result ForgotPassword(EmailDto emailDto ,int sentCode);
        public Result RestorePassword(RestorePasswordDto restorePasswordDto);
        public Result RefreshToken(UserDto userDto);
    }
}
