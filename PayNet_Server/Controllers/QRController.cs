using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PayNet_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRController : ControllerBase
    {
        [HttpGet("generate-qr/{customerId}")]
        public IActionResult GenerateQR(int customerId)
        {
            var data = $"paynet://pay?customerId={customerId}";
            var qrUrl = $"https://api.qrserver.com/v1/create-qr-code/?size=300x300&data={data}";
            return Ok(new { qrUrl });
        }

    }
}
