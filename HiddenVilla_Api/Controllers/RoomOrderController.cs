using Business.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

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
    }
}
