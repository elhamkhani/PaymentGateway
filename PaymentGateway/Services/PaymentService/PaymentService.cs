using Microsoft.Extensions.Logging;
using PaymentGateway.Services.BankService;
using PaymentGateway.Services.BankService.Models;
using PaymentGateway.Services.CosmosDbService;
using PaymentGateway.Services.PaymentService.Models;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IBankPaymentService _bankPaymentService;
        private readonly ICosmosDbService _cosmosDbService;
        public PaymentService(ILogger<PaymentService> logger, IBankPaymentService bankPaymentService, ICosmosDbService cosmosDbService)
        {
            _logger = logger;
            _bankPaymentService = bankPaymentService;
            _cosmosDbService = cosmosDbService;
        }

        public async Task<PaymentResponse> Pay(PaymentRequest model)
        {
            var bankPayment = new BankPaymentRequest
            {
                Amount = model.Amount,
                Currency = model.Currency,
                Cardnumber = model.Cardnumber,
                ExpiryMonth = model.ExpiryMonth,
                ExpiryYear = model.ExpiryYear
            };

            var result = await _bankPaymentService.ProcessPayment(bankPayment);


            if (result.status == BankPaymentProcessStatus.Success)
            {
                var paymentRecord = new PaymentRecord { Date = new DateTime(), Id = result.identifier, isSuccessfull = true, lastfourDigits = model.Cardnumber };

                await _cosmosDbService.AddItemAsync(paymentRecord);
                return new PaymentResponse
                {
                    identifier = result.identifier,
                    status = PaymentProcessStatus.Success
                };
            }
            else
            {
                return new PaymentResponse
                {
                   status = PaymentProcessStatus.Failure
                };
            }

        }

        public async Task<PaymentRecord> Retrieve(string id)
        {
            return await _cosmosDbService.GetItemAsync(id);
        }
    }
}
