using PaymentGateway.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaymentGateway.Services.PaymentService.Models
{
    public class PaymentRequest
    {
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
