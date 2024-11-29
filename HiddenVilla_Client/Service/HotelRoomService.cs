using HiddenVilla_Client.Model.Const;
using HiddenVilla_Client.Service.IService;
using Models.DTO;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace HiddenVilla_Client.Service
{
    public class HotelRoomService : IHotelRoomService
    {
        private readonly HttpClient httpClient;

        public HotelRoomService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HotelRoomDTO> GetHotelRoomDetails(int roomId, string checkInDate, string checkOutDate)
        {
            var response = await httpClient.GetAsync($"api/hotelroom/getdetails{roomId}?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var rooms = JsonConvert.DeserializeObject<HotelRoomDTO>(content);
                return rooms;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(error.ErrorMessage);
            }
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string checkInDate, string checkOutDate)
        {
            var response = await httpClient.GetAsync($"api/hotelroom/get_list?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            var content = await response.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<IEnumerable<HotelRoomDTO>>(content);

            return rooms;
        }
    }
}
