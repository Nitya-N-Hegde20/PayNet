using PayNet_Server.DTOs;

namespace PayNet_Server.Repository
{
    public interface IBankRepository
    {
        Task<List<BankDto>> GetBanksAsync();

    }
}
