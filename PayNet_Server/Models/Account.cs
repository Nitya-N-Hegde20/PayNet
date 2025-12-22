namespace PayNetServer.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string IFSC { get; set; }
        public decimal Balance { get; set; }
        public int CustomerId { get; set; }
        public bool IsActive { get; set; }

    }
}
