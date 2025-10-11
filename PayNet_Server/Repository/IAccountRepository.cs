using PayNet_Server.DTOs;
using PayNetServer.Models;

namespace PayNet_Server.Repository
{
    public interface IAccountRepository
    {
        Task<AccountDto?> CreateAccountAsync(CreateAccountDto account);
    }
}
