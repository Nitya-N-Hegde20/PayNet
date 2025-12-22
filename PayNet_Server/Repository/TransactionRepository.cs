using Dapper;
using Microsoft.AspNetCore.Mvc;
using PayNet_Server.DTOs;
using System.Data;

namespace PayNet_Server.Repository
{
    public class TransactionRepository:ITransactionRepository
    {
        private readonly IDbConnection _db;

        public TransactionRepository(IDbConnection db)
        {
            _db = db;
        }
        public async Task<string> SendMoneyAsync(SendMoneyDto dto)
        {
            return await _db.QuerySingleAsync<string>(
                "SendMoney",
                dto,
                commandType: CommandType.StoredProcedure
            );
        }



    }
}
