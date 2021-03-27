using PaymentGateway.Services.BankService.Models;
using System.Threading.Tasks;

namespace PaymentGateway.Services.BankService
{
    public interface IBankPaymentService
    {
       Task<BankPaymentResponse> ProcessPayment(BankPaymentRequest paymentRequest);
    }
}