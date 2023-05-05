using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRentApp.DTO
{
    [NotMapped]
    public class UserRegister
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        [RegularExpression(@"^\+994 (70|77|50|55|90) \d{3} \d{2} \d{2}$", ErrorMessage = "Phone number must be in the format +994 (70|77|50|55|90) ___ __ __")]
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
