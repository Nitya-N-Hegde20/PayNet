using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayNet_Server.Repository;

namespace PayNet_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankRepository _repo;

        public BankController(IBankRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetBankList()
        {
            try
            {
                var data = await _repo.GetBanksAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }

        }
    }
}
