using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PayNet_Server.Repository;

namespace PayNet_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ICustomerRepository _repo;

        public ContactController(ICustomerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("paynet/{customerId}")]
        public async Task<IActionResult> GetPayNetContacts(int customerId)
        {
            var contacts = await _repo.GetPayNetContactsAsync(customerId);

            return Ok(contacts.Select(c => new
            {
                id = c.Id,
                name = c.FullName,
                phone = c.Phone,
                email = c.Email
            }));
        }
    }
}
