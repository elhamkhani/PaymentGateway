using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using PaymentGateway.Services.BankService;
using PaymentGateway.Services.BankService.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.Tests
{
    public class BankPaymentServiceTests
    {
        private IBankPaymentService _sut;
        private readonly Mock<ILogger<BankPaymentService>> _logger;

        private readonly BankPaymentRequest validRequest = new BankPaymentRequest
        {
            Amount = 100,
            Currency = "GBP",
            CardNumber = "1111222233334444",
            ExpiryMonth = 10,
            ExpiryYear = 21
        };

        public BankPaymentServiceTests()
        {
            _logger = new Mock<ILogger<BankPaymentService>>();
        }

        [Fact]
        public async void Should_Call_Pay_Api()
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""identifier"": ""d54c1c7c-2c02-4958-9596-2b9d0e4971ef"", ""status"": ""Success""}"),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var uri = new Uri("http://test.com/");

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = uri,
            };

            _sut = new BankPaymentService(_logger.Object, httpClient);

            var payment = await _sut.ProcessPayment(validRequest);

            Assert.NotNull(payment);

            handlerMock.Protected().Verify("SendAsync",
                                            Times.Exactly(1),
                                            ItExpr.Is<HttpRequestMessage>(req =>
                                               req.Method == HttpMethod.Post
                                               && req.RequestUri == uri),
                                            ItExpr.IsAny<CancellationToken>()
                                         );
        }
    }
}
