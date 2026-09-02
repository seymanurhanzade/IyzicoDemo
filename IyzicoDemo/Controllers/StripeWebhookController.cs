//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using IyzicoDemo.Services;
//using IyzicoDemo.Settings;

//namespace IyzicoDemo.Controllers
//{
//    [ApiController]
//    [Route("api/stripe-webhook")]
//    public class StripeWebhookController : ControllerBase
//    {
//        private readonly IyzicoOption _IyzicoOptions;
//        private readonly IPaymentService _paymentService;

//        public StripeWebhookController(
//            IOptions<IyzicoOption> IyzicoOptions,
//            IPaymentService paymentService)
//        {
//            _IyzicoOptions = IyzicoOptions.Value;
//            _paymentService = paymentService;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Handle()
//        {
//            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
//            var stripeSignature = Request.Headers["Stripe-Signature"];

//            Event stripeEvent;
//            try
//            {
//                stripeEvent = EventUtility.ConstructEvent(
//                    json,
//                    stripeSignature,
//                    _IyzicoOptions.WebhookSecret
//                );
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Webhook error: {ex.Message}");
//                return BadRequest();
//            }

//            if (stripeEvent.Type == "checkout.session.completed")
//            {
//                var session = stripeEvent.Data.Object as Session;

//                if (session != null)
//                {
//                    await _paymentService.HandleCheckoutCompletedAsync(session.Id);
//                }
//            }

//            return Ok();
//        }
//    }
//}
