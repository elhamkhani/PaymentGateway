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

            if (result.status == PaymentProcessStatus.Success)
                return Ok(new { identifier = result.identifier, status = result.status });
            else
                return StatusCode(500);
        }

        [HttpGet]
        [Route("Retrieve")]
        [Description("Retrieve thedetails of a previously made payment.")]
        public IActionResult Retrieve()
        {
            return Ok(new { identifier = "" });
        }
    }
}
