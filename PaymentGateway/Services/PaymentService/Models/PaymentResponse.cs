namespace PaymentGateway.Services.PaymentService.Models
{
    public class PaymentResponse
    {
        public string identifier { get; set; }
        public PaymentProcessStatus status { get; set; }
        public string errorMessage { get; set; }
  
    }
}