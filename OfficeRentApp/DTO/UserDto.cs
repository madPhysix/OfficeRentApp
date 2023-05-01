﻿using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRentApp.DTO
{
    [NotMapped]
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}