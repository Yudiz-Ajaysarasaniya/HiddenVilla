using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelAmenityRepository
    {
        public Task<HotelAmenityDTO> CreateAmenity(HotelAmenityDTO amenityDTO);
        public Task<HotelAmenityDTO> UpdateAmenity(int amenityId, HotelAmenityDTO amenityDTO);
        public Task<int> DeleteAmenityById(int amenityId);
        public Task<HotelAmenityDTO> GetAmenity(int amenityId);
        public Task<IEnumerable<HotelAmenityDTO>> GetAllAmenity();
        public Task<HotelAmenityDTO> IsUnique(string name, int id);
    }
}
