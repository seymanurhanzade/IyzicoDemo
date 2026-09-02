using Microsoft.AspNetCore.Mvc;
using IyzicoDemo.Models;
using IyzicoDemo.Services;

namespace IyzicoDemo.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class IyzicoController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public IyzicoController(IPaymentService paymentservice)
        {
            _paymentService = paymentservice;
        }

        [HttpPost("checkout/success")]
        public async Task<IActionResult> Success(CreateCheckoutRequest request)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
            var result = await _paymentService.CreateCheckoutSessionAsync(request, baseUrl);
            return Ok(result);
        }

        //[HttpGet("checkout/cancel")]
        //public IActionResult Cancel()
        //{
        //    return Ok();
        //}
    }
}
