using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Services.PaymentService.Models
{
    public class PaymentRecord
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string lastfourDigits { get; set; }
        public bool isSuccessfull { get; set; }
        public DateTime Date { get; set; }

    }
}
