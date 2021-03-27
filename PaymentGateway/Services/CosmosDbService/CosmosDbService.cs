using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using PaymentGateway.Services.PaymentService.Models;

namespace PaymentGateway.Services.CosmosDbService
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
           CosmosClient dbClient,
           string databaseName,
           string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(PaymentRecord item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task<PaymentRecord> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<PaymentRecord> response = await _container.ReadItemAsync<PaymentRecord>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
