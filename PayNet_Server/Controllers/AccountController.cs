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
            if (dto.InitialBalance < 0)
                return BadRequest("Initial balance cannot be negative.");

            var account = await _accountRepository.CreateAccountAsync(dto);

            if (account == null)
                return StatusCode(500, "Error creating account.");

            return Ok(account);
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
