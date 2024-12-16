using Business.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Stripe;
using Stripe.Checkout;
namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomOrderController : ControllerBase
    {
        private readonly IRoomOrderDetailsRepository roomOrder;

        public RoomOrderController(IRoomOrderDetailsRepository roomOrder)
        {
            this.roomOrder = roomOrder;
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

        #region
        [HttpPost("paymentsuccess")]
        public async Task<IActionResult> PaymentSuccess([FromBody] RoomOrderDetailsDTO orderDetailsDTO)
        {
            orderDetailsDTO.HotelRoomDTO.ImageUrls = [];
            var services = new SessionService();
            var sessionDetails = services.Get(orderDetailsDTO.StripeSessionId);
            if(sessionDetails.PaymentStatus.ToLower() == "paid")
            {
                var result = await roomOrder.PaymentStatus(orderDetailsDTO.Id);
                if(result == null)
                {
                    return BadRequest(new ErrorModel
                    {
                        ErrorMessage = "Can not update payment status"
                    });
                }
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
