using PaymentGateway.Enums;

namespace PaymentGateway.Services.BankService.Models
{
    public class BankPaymentRequest
    {
        public string Cardnumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
