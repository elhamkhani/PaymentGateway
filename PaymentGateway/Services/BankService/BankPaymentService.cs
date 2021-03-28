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
        private readonly ILogger<BankPaymentService> _logger;
        private readonly HttpClient _client;


        public BankPaymentService(ILogger<BankPaymentService> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<BankPaymentResponse> ProcessPayment(BankPaymentRequest paymentRequest)
        {
            var requestMessage = new HttpRequestMessage
            { 
                Method = HttpMethod.Post,
                Content = new StringContent(
                JsonConvert.SerializeObject(paymentRequest),
                Encoding.UTF8,
                "application/json")
            };

            var result = await _client.SendAsync(requestMessage);
            
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();

                var responseContent = JsonConvert.DeserializeObject<BankPaymentResponse>(response);

                return new BankPaymentResponse { Identifier = responseContent.Identifier, Status = responseContent.Status };
            }
            else
            {
                _logger.LogError($"Payment failed: {paymentRequest.CardNumber.Mask(4)} + {paymentRequest.Amount} + {paymentRequest.Currency}");

                return new BankPaymentResponse { Status = BankPaymentProcessStatus.Failure };
            }

        }
    }
}
