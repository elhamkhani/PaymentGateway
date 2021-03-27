using PaymentGateway.Services.PaymentService.Models;
using System.Threading.Tasks;

namespace PaymentGateway.Services.PaymentService
{
   public interface IPaymentService
    {
        Task<PaymentResponse> Pay(PaymentRequest paymentRequest);
        Task<PaymentRecord> Retrieve(string id);
    }
}
