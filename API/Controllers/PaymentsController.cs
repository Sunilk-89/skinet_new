using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Order = Core.Entities.OrderAggregate.Order;

namespace API.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<IPaymentService> _logger;
        private const string WhSecret = "whsec_a0666fb2379e0cee0276f1a0e237e5f32264c07fb4d0cc31edb3fcfa6e114fa2";

        public PaymentsController(IPaymentService paymentService, ILogger<IPaymentService> logger)
        {
            _paymentService = paymentService;   
            _logger = logger;
        }

        [Authorize]
        [HttpPost("{basketId}")]

        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            
            var basket =  await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if(basket == null ) return BadRequest(new ApiResponse(400,"Problem with your basket"));
            return basket;
        }

        [HttpPost("webhook")]

        public async Task<ActionResult> StripeWebhook()
        {
                var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

                var stripeEvent = EventUtility.ConstructEvent(json,Request.Headers["Stripe-Signature"],WhSecret);

                PaymentIntent intent;
                Order order;

                switch (stripeEvent.Type)
                {
                    case "payment_intent.succeeded":
                        {
                                intent = (PaymentIntent)stripeEvent.Data.Object;
                                _logger.LogInformation("Payment Succeeded",intent.Id);
                                order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                                _logger.LogInformation("Order updated to payment received",order.Id);
                        }break;
                    
                    case "payment_intent.payment_failed":
                    {
                            intent =(PaymentIntent)stripeEvent.Data.Object;
                            _logger.LogInformation("Payment Failed",intent.Id);
                           order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                                _logger.LogInformation("Payment Failed",order.Id);
                    }break;
                }
                return new EmptyResult();
        }

    }
}