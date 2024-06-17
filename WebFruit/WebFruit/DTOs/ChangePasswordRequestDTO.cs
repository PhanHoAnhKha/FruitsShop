namespace WebFruit.DTOs
{
    public class ChangePasswordRequestDTO
    {
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
