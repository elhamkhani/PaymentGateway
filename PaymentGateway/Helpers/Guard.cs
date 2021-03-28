using PaymentGateway.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Helpers
{
    public static class Guard
    {
        public static void CurrnecyNotFound(this Currency currency)
        {
            if (currency == Currency.NONE) throw new ArgumentOutOfRangeException(nameof(currency));
        }
    }
}
