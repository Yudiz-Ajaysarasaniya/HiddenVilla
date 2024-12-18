using Business.Repository.IRepository;
using HiddenVilla.notify;
using HiddenVilla_Api.Helper;
using HiddenVilla_Client.Model.Const;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Request;
using Stripe;
using Stripe.Checkout;
namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomOrderController : ControllerBase
    {
        private readonly IRoomOrderDetailsRepository roomOrder;
        private readonly StripeClient stripeClient;
        private readonly IMessages messages;

        public RoomOrderController(IRoomOrderDetailsRepository roomOrder, StripeClient stripeClient, IMessages messages)
        {
            this.roomOrder = roomOrder;
            this.stripeClient = stripeClient;
            this.messages = messages;
        }

        #region Get All RoomOrder Details
        [HttpGet("get_order_list")]
        public async Task<IActionResult> GetAllRoomOrderDetails()
        {
            var response = await roomOrder.GetAllRoomOrderDetails();

            if (response == null) 
            {
                return NotFound();
            }

            return Ok(response);
        }
        #endregion

        #region Craete Order
        [HttpPost("create_order")]
        public async Task<IActionResult> CreateOrder(RoomOrderDetailsDTO orderDetailsDTO)
        {
            if(ModelState.IsValid)
            {
                var result = await roomOrder.Create(orderDetailsDTO);
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel
                {
                    ErrorMessage = "Error while creating booking"
                });
            }
        }
        #endregion

        #region Payment Success
        [HttpPost("paymentsuccess")]
        public async Task<IActionResult> PaymentSuccess([FromBody] PaymentSuccessRequest paymentSuccessRequest)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorModel
                {
                    ErrorMessage = "Invalid model data",
                });
            }

            if (string.IsNullOrEmpty(paymentSuccessRequest.StripeSessionId))
            {
                return BadRequest(new ErrorModel
                {
                    ErrorMessage = "Invalid Stripe session ID",
                });
            }

            var services = new SessionService(stripeClient);
            var sessionDetails = services.Get(paymentSuccessRequest.StripeSessionId);
            if(sessionDetails.PaymentStatus.ToLower() == "paid")
            {
                var result = await roomOrder.PaymentStatus(paymentSuccessRequest.Id);
                if(result == null)
                {
                    return BadRequest(new ErrorModel
                    {
                        ErrorMessage = "Can not update payment status"
                    });
                }

                var email = "ajay.sarasaniya@yudizsolutions.com";
                new EmailHelper(this.messages).SendPaymentSuccessEmail(email);

                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel
                {
                    ErrorMessage = "Can not update payment status"
                });
            }
        }
        #endregion
    }
}
