using PaymentGateway.Services.PaymentService;
using Xunit;
using Moq;
using PaymentGateway.Services.BankService;
using PaymentGateway.Enums;
using PaymentGateway.Services.PaymentService.Models;
using Microsoft.Extensions.Logging;
using PaymentGateway.Services.CosmosDbService;
using PaymentGateway.Services.BankService.Models;

namespace PaymentGateway.Tests
{
    public class PaymentServiceTests
    {
        private IPaymentService _sut;
        private readonly Mock<IBankPaymentService> _bankServiceMock;
        private readonly Mock<ILogger<PaymentService>> _logger;
        private readonly Mock<ICosmosDbService> _cosmosDbService;

        private readonly PaymentRequest validRequest = new PaymentRequest
        {
            Amount = 100,
            Currency = Currency.GBP,
            CardNumber = "1111222233334444",
            ExpiryMonth = 10,
            ExpiryYear = 21
        };

        private readonly PaymentRequest inValidRequest = new PaymentRequest
        {
            Amount = -100,
            Currency = Currency.GBP,
            CardNumber = "1111222233334444",
            ExpiryMonth = 10,
            ExpiryYear = 21
        };

        public PaymentServiceTests()
        {
            _bankServiceMock = new Mock<IBankPaymentService>();
            _logger = new Mock<ILogger<PaymentService>>();
            _cosmosDbService = new Mock<ICosmosDbService>();

            _sut = new PaymentService(_logger.Object, _bankServiceMock.Object, _cosmosDbService.Object);
        }

        [Fact]
        public async void Given_Vaild_Request_Should_Payment_Be_Successful()
        {
            var bankPaymentResponse = new BankPaymentResponse
            {
                Identifier = "123456",
                Status = BankPaymentProcessStatus.Success
            };

            _bankServiceMock
                         .Setup(x => x.ProcessPayment(It.IsAny<BankPaymentRequest>()))
                         .ReturnsAsync(bankPaymentResponse)
                         .Verifiable();

            var actual = await _sut.Pay(validRequest);

            Assert.NotNull(actual.Identifier);
            Assert.Equal(PaymentProcessStatus.Success, actual.Status);
            Assert.Null(actual.ErrorMessage);

            _bankServiceMock.Verify(
               x => x.ProcessPayment(It.IsAny<BankPaymentRequest>()), Times.Once);
        }

        [Fact]
        public async void Given_Vaild_Request_Should_Payment_Be_Recorded()
        {
            var bankPaymentResponse = new BankPaymentResponse
            {
                Identifier = "123456",
                Status = BankPaymentProcessStatus.Success
            };

            _bankServiceMock
                         .Setup(x => x.ProcessPayment(It.IsAny<BankPaymentRequest>()))
                         .ReturnsAsync(bankPaymentResponse)
                         .Verifiable();

            var actual = await _sut.Pay(validRequest);

           _cosmosDbService.Verify(
              x => x.AddItemAsync(It.IsAny<PaymentRecord>()), Times.Once);
        }

        [Fact]
        public async void Given_Bank_Service_Fails_Payment_Should_Return_Error()
        {
            var bankPaymentResponse = new BankPaymentResponse
            {
                Status = BankPaymentProcessStatus.Failure
            };

            _bankServiceMock
                         .Setup(x => x.ProcessPayment(It.IsAny<BankPaymentRequest>()))
                         .ReturnsAsync(bankPaymentResponse)
                         .Verifiable();

            var actual = await _sut.Pay(validRequest);

            Assert.NotNull(actual.Identifier);
            Assert.Equal(PaymentProcessStatus.Failure, actual.Status);
            Assert.Equal("Payment failed", actual.ErrorMessage);

            _cosmosDbService.Verify(
              x => x.AddItemAsync(It.IsAny<PaymentRecord>()), Times.Once);

        }
    }
}
