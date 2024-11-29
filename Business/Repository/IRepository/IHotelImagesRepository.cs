using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelImagesRepository
    {
        public Task<int> CreateRoomImage(HotelRoomImageDTO image);
        public Task<int> DeleteRoomImageByImageId(int imageId);
        public Task<int> DeleteRoomImageByRoomId(int roomId);
        public Task<int> DeleteRoomImageByImageUrl(string imageUrl);
        public Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId);
    }
}
