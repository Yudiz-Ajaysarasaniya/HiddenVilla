using Models.DTO;
using Models.Request;

namespace HiddenVilla_Client.Service.IService
{
    public interface IRoomOrderDetailsService
    {
        public Task<RoomOrderDetailsDTO> SaveRoomOrderDetails(RoomOrderDetailsDTO roomOrderDetails);
        public Task<PaymentSuccessRequest> MarkPaymentSuccess(PaymentSuccessRequest paymentSuccessRequest);
    }
}
