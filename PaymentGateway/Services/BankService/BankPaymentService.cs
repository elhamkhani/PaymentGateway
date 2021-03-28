using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentGateway.Helpers;
using PaymentGateway.Services.BankService.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace PaymentGateway.Services.BankService
{
    public class BankPaymentService : IBankPaymentService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<BankPaymentService> _logger;

        public BankPaymentService(ILogger<BankPaymentService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<BankPaymentResponse> ProcessPayment(BankPaymentRequest paymentRequest)
        {
            using var client = new HttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(paymentRequest), Encoding.UTF8, "application/json");
            
            var result = await client.PostAsync(_config["BankPaymentEndpoint"], content);
            
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();

                var responseContent = JsonConvert.DeserializeObject<BankPaymentResponse>(response);

                return new BankPaymentResponse { identifier = responseContent.identifier, status = BankPaymentProcessStatus.Success };
            }
            else
            {
                _logger.LogError($"Payment failed: {paymentRequest.CardNumber.Mask(4)} + {paymentRequest.Amount} + {paymentRequest.Currency}");

                return new BankPaymentResponse { status = BankPaymentProcessStatus.Failure };
            }

        }
    }
}
