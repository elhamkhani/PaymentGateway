using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Controllers;
using PaymentGateway.Services.PaymentService;
using PaymentGateway.Services.PaymentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.Tests.Controllers
{
    public class PaymentControllerTests
    {
        private PaymentController _sut;
        private readonly Mock<ILogger<PaymentController>> _logger;
        private readonly Mock<IPaymentService> _paymentService;

        public PaymentControllerTests()
        {
            _logger = new Mock<ILogger<PaymentController>>();
            _paymentService = new Mock<IPaymentService>();
            
        }

        [Fact]
        public async void Given_Invalid_Currency_Pay_API_Should_Throw_ArgumentException()
        {
            var paymentRequest = new PaymentRequest
            {
                Amount=1,
                CardNumber="123",
                Currency = Enums.Currency.NONE,
                ExpiryMonth = 10,
                ExpiryYear = 20
            };

            _sut = new PaymentController(_logger.Object, _paymentService.Object);
            var response = await _sut.Pay(paymentRequest);
            Assert.IsType<ObjectResult>(response);
        }

        [Fact]
        public async void Given_Valid_Request_Should_Return_Ok()
        {
            var paymentResponse = new PaymentResponse
            {
                Identifier = "123",
                Status = PaymentProcessStatus.Success
            };

            _paymentService
                      .Setup(x => x.Pay(It.IsAny<PaymentRequest>()))
                      .ReturnsAsync(paymentResponse)
                      .Verifiable();

            _sut = new PaymentController(_logger.Object, _paymentService.Object);

            var paymentRequest = new PaymentRequest
            {
                Amount = 1,
                CardNumber = "123",
                Currency = Enums.Currency.AED,
                ExpiryMonth = 10,
                ExpiryYear = 20
            };


            var response = await _sut.Pay(paymentRequest);

            Assert.IsType<OkObjectResult>(response);
        }
    }
}
