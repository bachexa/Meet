namespace WebApp.Models.Auth
{
    public class RegisterViewModel
    {
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}