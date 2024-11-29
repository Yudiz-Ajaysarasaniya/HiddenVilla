using HiddenVilla_Client.Model.Const;
using HiddenVilla_Client.Service.IService;
using Models.DTO;
using Newtonsoft.Json;

namespace HiddenVilla_Client.Service
{
    public class HotelAmenityService : IHotelAmenityService
    {
        private readonly HttpClient httpClient;

        public HotelAmenityService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<HotelAmenityDTO>> GetAllAmenity()
        {
            var response = await httpClient.GetAsync(ActionConst.GetAllAmenity);
            var content = await response.Content.ReadAsStringAsync();
            var amenity = JsonConvert.DeserializeObject<IEnumerable<HotelAmenityDTO>>(content);
            return amenity;
        }
    }
}
