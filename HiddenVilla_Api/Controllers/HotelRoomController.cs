using Business.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using System.Globalization;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomController : ControllerBase
    {
        private readonly IHotelRoomRepository roomRepository;

        public HotelRoomController(IHotelRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        #region Get HotelRoom List
        //[Authorize(Roles = Roles.Role_Admin)]
        [HttpGet("get_list")]
        public async Task<IActionResult> GetHotelRooms(string checkInDate = null, string checkOutDate = null)
        {
            if (string.IsNullOrWhiteSpace(checkInDate) || string.IsNullOrWhiteSpace(checkOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be supplied"
                });
            }

            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtcheckInDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid checkIn Date format. valid format will be MM/dd/yyyy"
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtcheckOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid checkOut Date format. valid format will be MM/dd/yyyy"
                });
            }
            var response = await roomRepository.GetAllHotelRooms(checkInDate, checkOutDate);

            return Ok(response);
        }
        #endregion

        #region Get Single Room
        [HttpGet("getdetails" + "{roomId}")]
        public async Task<IActionResult> GetRoomById(int roomId, string checkInDate = null, string checkOutDate = null)
        {
            if (roomId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid room Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            if (string.IsNullOrWhiteSpace(checkInDate) || string.IsNullOrWhiteSpace(checkOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be supplied"
                });
            }

            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtcheckInDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid checkIn Date format. valid format will be MM/dd/yyyy"
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtcheckOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid checkOut Date format. valid format will be MM/dd/yyyy"
                });
            }

            var response = await roomRepository.GetHotelRoom(roomId, checkInDate, checkOutDate);
            if (response == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Room not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(response);
        }

        #endregion
    }
}
