using HiddenVilla_Client.Model.Const;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Response.Base;
using Stripe;
using Stripe.Checkout;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly StripeClient stripeClient;

        public PaymentController(IConfiguration configuration, StripeClient stripeClient)
        {
            this.configuration = configuration;
            this.stripeClient = stripeClient;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Create(StripePeymentDTO request)
        {
            try
            {
                var domain = configuration.GetValue<string>("Client_URL");

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = request.Amount, //convert to cents
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = request.ProductName
                                }
                            },
                            Quantity = 1
                        }
                    },
                    Mode = "payment",
                    SuccessUrl = domain + "/success-payment?sessionid={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = domain + request.ReturnUrl
                };

                var service = new SessionService(stripeClient);
                Session session = await service.CreateAsync(options);

                return Ok(new SuccessResponse
                {
                    Data = session.Id
                });
            }
            catch(Exception e)
            {
                return BadRequest(new ErrorModel
                {
                    ErrorMessage = e.Message
                });
            }
        }
    }
}
