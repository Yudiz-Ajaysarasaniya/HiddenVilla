using Models.DTO;

namespace HiddenVilla_Client.Service.IService
{
    public interface IRoomOrderDetailsService
    {
        public Task<RoomOrderDetailsDTO> SaveRoomOrderDetails(RoomOrderDetailsDTO roomOrderDetails);
        public Task<RoomOrderDetailsDTO> MarkPaymentSuccess(RoomOrderDetailsDTO roomOrderDetails);
    }
}
