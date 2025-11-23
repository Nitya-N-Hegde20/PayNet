using PayNet_Server.DTOs;
using PayNetServer.Models;

namespace PayNet_Server.Repository
{
    public interface IAccountRepository
    {
        Task<string> CreateAccountAsync(CreateAccountDto account);

        Task<bool>DeleteAccountAsync (string accountNumber);
        Task<decimal> GetBalanceAsync(int customerId);
    }
}
