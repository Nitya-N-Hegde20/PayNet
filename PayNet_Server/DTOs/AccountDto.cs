namespace PayNet_Server.DTOs
{
    public class AccountDto
    {
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
