using Microsoft.AspNetCore.Mvc;
using PayNet_Server.Repository;
using PayNetServer.DTOs;
using PayNetServer.Models;

namespace PayNetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public AuthController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Registers customer if that customer not exists already and encrypts password
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _repository.GetCustomerByEmailAsync(dto.Email);
            if (existing != null)
                return BadRequest("Email already exists.");

            var customer = new Customer
            {
                FullName = dto.FullName,
                Address = dto.Address,
                Email = dto.Email,
                Phone = dto.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsActive = true
            };

            var result = await _repository.RegisterCustomerAsync(customer);

            if (result == -1)
                return BadRequest("Email already exists.");

            return Ok("Registration successful");
        }
    }
}
