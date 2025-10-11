using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PayNet_Server.DTOs;
using PayNet_Server.Repository;
using PayNetServer.DTOs;
using PayNetServer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PayNetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly IConfiguration _config;

        public AuthController(ICustomerRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
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
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsActive = true
            };

            var result = await _repository.RegisterCustomerAsync(customer);

            if (result == -1)
                return BadRequest("Email already exists.");

            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                return BadRequest("Email and password are required");

            var customer = await _repository.GetCustomerByEmailAsync(dto.Email);

            if (customer == null)
                return Unauthorized("Invalid email or password");

            // Verify password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, customer.Password);
            if (!isPasswordValid)
                return Unauthorized("Invalid email or password");

            // Generate JWT token
            var token = GenerateJwtToken(customer);

            return Ok(new
            {
                Token = token,
                CustomerName = customer.FullName,
                Email = customer.Email
            });
        }

        private string GenerateJwtToken(Customer customer)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, customer.Email ?? ""),
                new Claim("FullName", customer.FullName),
                new Claim("CustomerId", customer.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

