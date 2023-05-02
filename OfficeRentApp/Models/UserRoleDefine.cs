using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRentApp.Models
{
    public class UserRoleDefine
    {
        public int Id { get; set; }
        public string Role { get; set; } = String.Empty;
        public List<User>? users { get; set; }
    }
}
