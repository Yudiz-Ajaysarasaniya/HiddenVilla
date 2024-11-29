using Business.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelAmenityController : ControllerBase
    {
        private readonly IHotelAmenityRepository amenityRepository;

        public HotelAmenityController(IHotelAmenityRepository amenityRepository)
        {
            this.amenityRepository = amenityRepository;
        }

        [HttpGet("get_list")]
        public async Task<IActionResult> GetAmenityList()
        {
            var response = await amenityRepository.GetAllAmenity();

            if(response == null)
            {
                return NotFound(new ErrorModel
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "No amenities found"
                });
            }
            return Ok(response);
        }
    }
}
