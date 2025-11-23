using Google.Apis.Auth;
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

            return Ok(new { message = "Registration successful" });
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
                Customer=customer
            });
        }
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto dto)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken);

                var existing = await _repository.GetCustomerByEmailAsync(payload.Email);
                if (existing == null)
                {
                    return Ok(new
                    {
                        requiresPhone = true,
                        FullName = payload.Name,
                        Email = payload.Email
                    });
                }

                var token = GenerateJwtToken(existing);
                return Ok(new
                {
                    Token = token,
                    Customer = existing,
                    requiresPhone = false
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Google login failed: {ex.Message}");
            }
        }

        [HttpPost("complete-google-registration")]
        public async Task<IActionResult> CompleteGoogleRegistration([FromBody] GoogleRegistrationDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest("Email is required");

            var existing = await _repository.GetCustomerByEmailAsync(dto.Email);
            if (existing != null)
                return BadRequest("User already exists");

            var customer = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = string.Empty, // optional for Google
                Password = string.Empty,
                IsActive = true
            };

            await _repository.RegisterCustomerAsync(customer);

            var savedCustomer = await _repository.GetCustomerByEmailAsync(dto.Email);
            var token = GenerateJwtToken(savedCustomer);

            return Ok(new { Token = token, Customer = savedCustomer });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDTO dto)
        {
            var updated = await _repository.UpdateCustomerAsync(new Customer
            {
                Id = dto.Id,
                FullName = dto.FullName,
                Address = dto.Address,
                Phone = dto.Phone
            });

            if (updated == null)
                return NotFound();

            return Ok(updated);
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

