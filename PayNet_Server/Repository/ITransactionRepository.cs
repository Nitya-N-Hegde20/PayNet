using Microsoft.AspNetCore.Mvc;
using PayNet_Server.DTOs;
using System.Data;

namespace PayNet_Server.Repository
{
    public interface ITransactionRepository
    {
        Task<string> SendMoneyAsync(SendMoneyDto dto);

    }
}
