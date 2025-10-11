using Dapper;
using Microsoft.Data.SqlClient;
using PayNet_Server.DTOs;
using PayNetServer.Models;
using System.Data;

namespace PayNet_Server.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnection _db;
        public AccountRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<AccountDto?> CreateAccountAsync(CreateAccountDto dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CustomerId", dto.CustomerId);
            parameters.Add("@InitialBalance", dto.InitialBalance);

            var result = await _db.QuerySingleAsync<AccountDto>("CreateAccount",
                parameters, commandType: CommandType.StoredProcedure);

            return result;

        }

        public async Task<bool> DeleteAccountAsync(string accountNumber)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AccountNumber", accountNumber);
            var result = await _db.QuerySingleAsync<bool>("DeleteAccount",
                parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
