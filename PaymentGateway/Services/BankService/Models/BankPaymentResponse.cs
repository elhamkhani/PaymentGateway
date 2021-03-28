namespace PaymentGateway.Services.BankService.Models
{
    public class BankPaymentResponse
    {
        public string Identifier { get; set; }
        public BankPaymentProcessStatus Status { get; set; }
    }
}