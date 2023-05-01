using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRentApp.Models
{
    public class UserRoleDefine
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public string Role { get; set; } = String.Empty;
    }
}
