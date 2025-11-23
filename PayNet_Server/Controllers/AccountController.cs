using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayNet_Server.DTOs;
using PayNet_Server.Repository;

namespace PayNet_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto dto)
        {
            if (string.IsNullOrEmpty(dto.IFSC))
                return BadRequest("IFSC required.");

            if (string.IsNullOrEmpty(dto.BankCode))
                return BadRequest("Bank Code required.");

            var accountNumber = await _accountRepository.CreateAccountAsync(dto);

            return Ok(new { message = "Account created", AccountNumber = accountNumber });
        }

        [HttpGet("balance/{customerId}")]
        public async Task<IActionResult> GetBalance(int customerId)
        {
            var balance = await _accountRepository.GetBalanceAsync(customerId);
            return Ok(new { balance });
        }



        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAccount([FromBody] string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
                return BadRequest("Account Number is required");

            var account = await _accountRepository.DeleteAccountAsync(accountNumber);

            if (account == null)
                return StatusCode(500, "Error deleting account.");

            return Ok(account);
        }
    }
}
