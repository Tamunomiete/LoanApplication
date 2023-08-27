namespace LoanApplication.DAL.Models
{
    public class CreateUserParams
    {

        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? IpAddress { get; set; }
        public string? SessionId { get; set; }
        public string? PasswordResetCode { get; set; }
        public string? ProfilePictureBase64 { get; set; } // Base64-encoded image data
        public string? ProfilePictureContentType { get; set; } // Image content type (e.g., "image/jpeg")
    }
}
