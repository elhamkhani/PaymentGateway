using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Helpers
{
    public static class Helper
    {
        public static string Mask(this string text, int lengthVisible)
        {
            return $"{new string('*', Math.Max(0, text.Length- lengthVisible))}{text.Substring(text.Length - lengthVisible)}";
        }
    }
}
