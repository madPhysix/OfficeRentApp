using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRentApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Token { get; set; }
        [ForeignKey("UserRoleDefine")]
        public int RoleId { get; set; } = 1;
        public UserRoleDefine Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public User()
        {

        }
    }
}
