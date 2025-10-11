using Microsoft.AspNetCore.Mvc;
using Moq;
using PayNet_Server.Controllers;
using PayNet_Server.DTOs;
using PayNet_Server.Repository;
using PayNetServer.Controllers;
using PayNetServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace PayNet_Test
{
    public class AccountControllerTests
    {
        Mock<IAccountRepository> mockRepo;
        AccountController controller;

        public AccountControllerTests()
        {
            mockRepo = new Mock<IAccountRepository>();
            controller = new AccountController(mockRepo.Object);
        }

        [Fact]
        public async Task CreateAccount_ReturnsBadRequest_WhenBalanceisNegative()
        {
            CreateAccountDto createAccountDto = new CreateAccountDto
            {
                CustomerId = 1,
                InitialBalance = -3
            };
            mockRepo.Setup(r => r.CreateAccountAsync(It.IsAny<CreateAccountDto>()))
                  .ReturnsAsync(It.IsAny<AccountDto>());

            var result = await controller.CreateAccount(createAccountDto);
            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Initial balance cannot be negative.", badRequest.Value);
        }

        [Fact]
        public async Task CreateAccount_ReturnsSuccess()
        {
            CreateAccountDto createAccountDto = new CreateAccountDto
            {
                CustomerId = 1,
                InitialBalance = 2000
            };
            mockRepo.Setup(r => r.CreateAccountAsync(It.IsAny<CreateAccountDto>()))
                  .ReturnsAsync(It.IsAny<AccountDto>());

            var result = await controller.CreateAccount(createAccountDto);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteAccount_ReturnsBadRequest_WhenANIsNull()
        {
            
            mockRepo.Setup(r => r.DeleteAccountAsync(It.IsAny<String>()))
                  .ReturnsAsync(It.IsAny<bool>());

            var result = await controller.DeleteAccount(null);
            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Account Number is required", badRequest.Value);
        }

        [Fact]
        public async Task DeleteAccount_ReturnsSuccess()
        {
            mockRepo.Setup(r => r.DeleteAccountAsync(It.IsAny<String>()))
                  .ReturnsAsync(It.IsAny<bool>());
            string accountNumber = "ACC251110915";
            var result = await controller.DeleteAccount(accountNumber);
            Assert.NotNull(result);
        }
    }
}
