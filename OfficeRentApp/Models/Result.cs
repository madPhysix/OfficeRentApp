namespace OfficeRentApp.Models
{
    public class Result
    {
        public object? Data { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; } = "Success";
    }
}
