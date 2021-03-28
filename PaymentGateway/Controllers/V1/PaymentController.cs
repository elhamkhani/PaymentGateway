using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Services.PaymentService;
using PaymentGateway.Services.PaymentService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;
     
        
        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        

        [HttpPost]
        [Route("Pay")]
        [Description("Process payment using details sent by merchant.")]
        public async Task<IActionResult> Pay([FromBody] PaymentRequest model)
        {

            var result = await _paymentService.Pay(model);

            if (result.Status == PaymentProcessStatus.Success)
                return Ok(new { identifier = result.Identifier, status = result.Status });
            else
                return StatusCode(500);
        }

        [HttpGet]
        [Route("Retrieve/{identifier}")]
        [Description("Retrieve thedetails of a previously made payment.")]
        public async Task<IActionResult> Retrieve(string identifier)
        {
            var result = await _paymentService.Retrieve(identifier);

            return Ok(new
            {
                date = result.PaymentDate,
                successfull = result.isSuccessfull,
                cardnumber = result.CardNumber,
                expiryMonth = result.ExpiryMonth,
                expiryYear = result.ExpiryYear,
                currency = result.Currency
            });
        }
    }
}
