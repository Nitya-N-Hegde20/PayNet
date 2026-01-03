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

        public async Task<string?> CreateAccountAsync(CreateAccountDto dto)
        {
            var p = new DynamicParameters();
            p.Add("@CustomerId", dto.CustomerId);
            p.Add("@BankName", dto.BankName);
            p.Add("@BankCode", dto.BankCode);
            p.Add("@BranchName", dto.BranchName);
            p.Add("@IFSC", dto.IFSC);
            p.Add("@Balance", dto.Balance);
            return await _db.QuerySingleAsync<string>(
                "CreateAccount",
                p,
                commandType: CommandType.StoredProcedure);

        }

        public async Task<decimal> GetBalanceAsync(int customerId)
        {
            return await _db.QueryFirstOrDefaultAsync<decimal>(
                "GetCustomerBalance",
                new { CustomerId = customerId },
                commandType: CommandType.StoredProcedure
            );
        }


        public async Task<bool> DeleteAccountAsync(string accountNumber)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AccountNumber", accountNumber);
            var result = await _db.QuerySingleAsync<bool>("DeleteAccount",
                parameters, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<List<Account>> GetAccountsByCustomerIdAsync(int customerId)
        {
            var result = await _db.QueryAsync<Account>(
                "GetAccountsByCustomerId",
                new { CustomerId = customerId },
                commandType: CommandType.StoredProcedure
            );

            return result.ToList();
        }

        public async Task<Account?> GetFirstAccountAsync(int customerId)
        {
            return await _db.QueryFirstOrDefaultAsync<Account>(
                "GetFirstAccountByCustomer",
                new { CustomerId = customerId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Account?> GetAccountDetailsAsync(string accountNumber)
        {
            return await _db.QueryFirstOrDefaultAsync<Account>(
                "GetAccountDetailsByAccountNumber",
                new { AccountNumber = accountNumber },
                commandType: CommandType.StoredProcedure
            );
        }

    }
}
