namespace PayNet_Server.DTOs
{
    public class SendMoneyDto
    {
        public string FromAccountId { get; set; }
        public string ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }

}
