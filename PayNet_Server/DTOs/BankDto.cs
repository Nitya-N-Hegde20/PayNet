namespace PayNet_Server.DTOs
{
    public class BankDto
    {
        public string name { get; set; }
        public string code { get; set; }
        public List<BranchDto> branches { get; set; }
    }
}
