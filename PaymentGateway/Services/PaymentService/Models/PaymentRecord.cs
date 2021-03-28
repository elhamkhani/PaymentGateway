using Newtonsoft.Json;
using PaymentGateway.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Services.PaymentService.Models
{
    public class PaymentRecord
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public bool isSuccessfull { get; set; }
        public DateTime PaymentDate { get; set; }
        [Required]
        [RegularExpression("[0-9]{16}")]
        public string CardNumber { get; set; }
        [Required]
        [RegularExpression("^(0[1-9]|1[0-2])$")]
        public int ExpiryMonth { get; set; }
        [Required]
        [RegularExpression("^(9[0-9]|2[0-9])$")]
        public int ExpiryYear { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public Currency Currency { get; set; }
    }
}
