namespace PaymentGateway.Services.PaymentService.Models
{
    public class PaymentResponse
    {
        public string Identifier { get; set; }
        public PaymentProcessStatus Status { get; set; }
        public string ErrorMessage { get; set; }
  
    }
}