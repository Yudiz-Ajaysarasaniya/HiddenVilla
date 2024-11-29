using Models.DTO;
using Models.Response.Base;

namespace HiddenVilla_Client.Service.IService
{
    public interface IPaymentService
    {
        public Task<SuccessResponse> CheckOut(StripePeymentDTO stripePeyment);
    }
}
