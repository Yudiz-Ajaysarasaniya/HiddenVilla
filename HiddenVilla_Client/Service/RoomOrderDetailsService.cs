using HiddenVilla_Client.Model.Const;
using HiddenVilla_Client.Service.IService;
using Models.DTO;
using Models.Response.Base;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace HiddenVilla_Client.Service
{
    public class RoomOrderDetailsService : IRoomOrderDetailsService
    {
        private readonly HttpClient httpClient;

        public RoomOrderDetailsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<RoomOrderDetailsDTO> MarkPaymentSuccess(RoomOrderDetailsDTO roomOrderDetails)
        {
            throw new NotImplementedException();
        }

        public async Task<RoomOrderDetailsDTO> SaveRoomOrderDetails(RoomOrderDetailsDTO roomOrderDetails)
        {
            roomOrderDetails.UserId = "dummy user";
            var content = JsonConvert.SerializeObject(roomOrderDetails);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(ActionConst.SaveOrder, bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RoomOrderDetailsDTO>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(error.ErrorMessage);
            }
        }
    }
}
