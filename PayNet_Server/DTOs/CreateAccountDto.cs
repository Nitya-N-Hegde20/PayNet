namespace PayNet_Server.DTOs
{
    public class CreateAccountDto
    {
        public int CustomerId { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string BranchName { get; set; }
        public string IFSC { get; set; }
        public decimal Balance { get; set; }
    }
}
