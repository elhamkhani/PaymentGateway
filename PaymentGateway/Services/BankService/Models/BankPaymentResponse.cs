namespace PaymentGateway.Services.BankService.Models
{
    public class BankPaymentResponse
    {
        public string identifier { get; set; }
        public BankPaymentProcessStatus status { get; set; }
    }
}