namespace PayNet_Server.DTOs
{
    public class GoogleLoginDto
    {
        public string IdToken { get; set; } = string.Empty;
    }

    public class GoogleRegistrationDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
