using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeRentApp.DTO;
using OfficeRentApp.Models;

namespace OfficeRentApp.Repositories.UserAuthRepositories
{
    public interface IUserAuth
    {

        public string RegisterUser(UserRegister registerRequest);
        public UserDto Login([FromBody] UserLogin userLogin);
    }
}
