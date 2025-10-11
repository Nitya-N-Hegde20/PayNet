using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using PayNet_Server.Repository;
using PayNetServer.Controllers;
using PayNetServer.DTOs;
using PayNetServer.Models;

namespace PayNet_Test
{
    public class AuthControllerTests
    {
        Mock<ICustomerRepository> mockRepo;
        AuthController controller;
        private readonly IConfiguration config;
        public AuthControllerTests()
        {
            mockRepo = new Mock<ICustomerRepository>();
            controller = new AuthController(mockRepo.Object, config);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenEmailExists()
        {
            // Arrange
            mockRepo.Setup(r => r.GetCustomerByEmailAsync("test@mail.com"))
                    .ReturnsAsync(new Customer { Email = "test@mail.com" });
            var dto = new RegisterDto
            {
                FullName = "Nitya",
                Address = "Bangalore",
                Email = "test@mail.com",
                Phone = "1234567890",
                Password = "abc@123"
            };

            // Act
            var result = await controller.Register(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Email already exists.", badRequest.Value);
        }

        [Fact]
        public async Task Register_ReturnsOk_WhenValidCustomer()
        {
            // Arrange
            mockRepo.Setup(r => r.GetCustomerByEmailAsync(It.IsAny<string>()))
                    .ReturnsAsync((Customer?)null);

            mockRepo.Setup(r => r.RegisterCustomerAsync(It.IsAny<Customer>()))
                    .ReturnsAsync(1);

            var dto = new RegisterDto
            {
                FullName = "Nitya",
                Address = "Bangalore",
                Email = "new@mail.com",
                Phone = "1234567890",
                Password = "abc@123"
            };

            // Act
            var result = await controller.Register(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Registration successful", okResult.Value);
        }
    }
}