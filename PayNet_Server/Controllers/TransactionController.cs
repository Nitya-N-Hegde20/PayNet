using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayNet_Server.DTOs;
using PayNet_Server.Repository;

namespace PayNet_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionRepository _repo;

        public TransactionController(ITransactionRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMoney([FromBody] SendMoneyDto dto)
        {
            try
            {
                var result = await _repo.SendMoneyAsync(dto);
                return Ok(new { message = "Transfer successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
