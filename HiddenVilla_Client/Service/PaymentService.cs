using HiddenVilla_Client.Model.Const;
using HiddenVilla_Client.Service.IService;
using Models.DTO;
using Models.Response.Base;
using Newtonsoft.Json;
using System.Text;

namespace HiddenVilla_Client.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient httpClient;

        public PaymentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<SuccessResponse> CheckOut(StripePeymentDTO stripePeyment)
        {
            var content = JsonConvert.SerializeObject(stripePeyment);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(ActionConst.Payment, bodyContent);

            if(response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SuccessResponse>(contentTemp);
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
