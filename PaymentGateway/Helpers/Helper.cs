using System;

namespace PaymentGateway.Helpers
{
    public static class Helper
    {
        public static string Mask(this string text, int lengthVisible)
        {
            var trimLength = Math.Max(0, text.Length - lengthVisible);
            return $"{new string('*', trimLength)}{text.Substring(trimLength)}";
        }
    }
}
