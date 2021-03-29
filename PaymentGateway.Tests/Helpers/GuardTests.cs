using PaymentGateway.Enums;
using PaymentGateway.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.Tests.Helpers
{
    public class GuardTests
    {
        [Fact]
        public void Given_Invalid_Currency_Should_Throw_ArgumentException()
        {
            var actual = Currency.NONE;
            
            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                () => Guard.CurrnecyNotFound(actual));

            Assert.Contains("currency", exception.Message);
        }
    }
}
