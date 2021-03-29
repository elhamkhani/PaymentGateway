using PaymentGateway.Services.BankService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.Tests.Models
{
    public class BankPaymentRequestTests
    {
        [Theory]
        [InlineData("1234567891234567", 10)]
        [InlineData("123",10)]

        public void Should_Construct_New_Instance(string CardNumber, decimal Amount)
        {
            var paymentRequest = new BankPaymentRequest
            {
                Amount = 10,
                CardNumber = CardNumber
            };

            Assert.NotNull(paymentRequest);
        }
    }
}
