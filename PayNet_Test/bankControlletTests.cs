using Microsoft.AspNetCore.Mvc;
using Moq;
using PayNet_Server.Controllers;
using PayNet_Server.DTOs;
using PayNet_Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayNet_Test
{
     public class bankControlletTests
    {
        Mock<IBankRepository> mockRepo;
        BankController controller;


        public bankControlletTests()
        {
            mockRepo = new Mock<IBankRepository>();
            controller = new BankController(mockRepo.Object);
        }

        [Fact]
        public async Task GetBankList_ReturnsBadRequest()
        {

            var banks = new List<BankDto>
        {
            new BankDto { name = "SBI", code = "SBI" },
            new BankDto { name = "HDFC", code = "HDFC" }
        };

            mockRepo.Setup(r => r.GetBanksAsync()).ReturnsAsync(banks);

            // Act
            var result = await controller.GetBankList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedList = Assert.IsType<List<BankDto>>(okResult.Value);

            Assert.Equal(2, returnedList.Count);
            Assert.Equal("SBI", returnedList[0].name);
        }

        [Fact]
        public async Task GetBankList_ReturnsOk_WhenEmpty()
        {
            // Arrange
            mockRepo.Setup(r => r.GetBanksAsync()).ReturnsAsync(new List<BankDto>());

            // Act
            var result = await controller.GetBankList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedList = Assert.IsType<List<BankDto>>(okResult.Value);

            Assert.Empty(returnedList);
        }

        [Fact]
        public async Task GetBankList_Returns500_OnException()
        {
            // Arrange
            mockRepo.Setup(r => r.GetBanksAsync())
                    .ThrowsAsync(new Exception("Error"));

            // Act
            var result = await controller.GetBankList();

            // Assert
            var statusCode = result as ObjectResult;
            Assert.Equal(500, statusCode?.StatusCode);
        }
    }
}
