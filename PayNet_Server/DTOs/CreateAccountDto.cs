namespace PayNet_Server.DTOs
{
    public class CreateAccountDto
    {
        public int CustomerId { get; set; }
        public decimal InitialBalance { get; set; }
    }
}
