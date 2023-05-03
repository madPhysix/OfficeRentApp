using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRentApp.Models
{
    public class UserRoleDefine
    {
        [Key]
        public int Id { get; set; }
        public string Role { get; set; }
        public List<User>? Users { get; set; }
        public UserRoleDefine()
        { 

        }
    }
}
