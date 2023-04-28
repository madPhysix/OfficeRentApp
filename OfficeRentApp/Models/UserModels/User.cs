namespace OfficeRentApp.Models.UserModels
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } = "User";
        public string Surname { get; set; }
        public string GivenName { get; set; }
    }
}
