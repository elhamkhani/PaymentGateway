using PaymentGateway.Helpers;
using System;
using Xunit;

namespace PaymentGateway.Tests
{
    public class HelperTests
    {
        [Theory]
        [InlineData("1234567891234567", "************4567")]
        [InlineData("sometext", "****text")]
        [InlineData("aa", "aa")]
        [InlineData("4444", "4444")]
        public void Given_CardNumber_Should_Return_Masked_CardNumber(string text, string expectedValue)
        {
            Assert.Equal(expectedValue, text.Mask(4));
        }
    }
}
