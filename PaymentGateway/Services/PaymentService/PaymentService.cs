using Microsoft.Extensions.Logging;
using PaymentGateway.Services.BankService;
using PaymentGateway.Services.BankService.Models;
using PaymentGateway.Services.CosmosDbService;
using PaymentGateway.Services.PaymentService.Models;
using System;
using System.Threading.Tasks;
using PaymentGateway.Helpers;

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
                CardNumber = model.CardNumber,
                ExpiryMonth = model.ExpiryMonth,
                ExpiryYear = model.ExpiryYear
            };

            var result = await _bankPaymentService.ProcessPayment(bankPayment);

            var isSuccess = result.status == BankPaymentProcessStatus.Success;
            var paymentRecord = new PaymentRecord
            {
                Id = result.identifier,
                PaymentDate = DateTime.Now,
                isSuccessfull = isSuccess,
                CardNumber = model.CardNumber.Mask(4),
                Amount = model.Amount,
                ExpiryMonth = model.ExpiryMonth,
                ExpiryYear = model.ExpiryYear,
                Currency = model.Currency
            };

            await _cosmosDbService.AddItemAsync(paymentRecord);
           
            _logger.LogError($"Payment was {result.status} for cardnumber {model.CardNumber}.");
            
            return new PaymentResponse
            {
                identifier = result.identifier,
                status = isSuccess? PaymentProcessStatus.Success: PaymentProcessStatus.Failure
            };
        }

        public async Task<PaymentRecord> Retrieve(string id)
        {
            return await _cosmosDbService.GetItemAsync(id);
        }
    }

}
