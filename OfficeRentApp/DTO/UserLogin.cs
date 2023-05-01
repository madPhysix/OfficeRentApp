using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRentApp.DTO
{
    [NotMapped]
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
