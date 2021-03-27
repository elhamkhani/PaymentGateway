using PaymentGateway.Models;
using PaymentGateway.Services.PaymentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Services.CosmosDbService
{
   public interface ICosmosDbService
    {
        Task<PaymentRecord> GetItemAsync(string id);
        Task AddItemAsync(PaymentRecord item);

    }
}
