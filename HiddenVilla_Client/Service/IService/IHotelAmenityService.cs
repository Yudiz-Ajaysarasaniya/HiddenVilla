using Models.DTO;

namespace HiddenVilla_Client.Service.IService
{
    public interface IHotelAmenityService
    {
        public Task<IEnumerable<HotelAmenityDTO>> GetAllAmenity();
    }
}
