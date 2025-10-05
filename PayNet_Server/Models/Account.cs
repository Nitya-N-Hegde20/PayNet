namespace PayNetServer.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerId { get; set; }
        public bool IsActive { get; set; }
        public decimal Balance { get; set; }

    }
}
