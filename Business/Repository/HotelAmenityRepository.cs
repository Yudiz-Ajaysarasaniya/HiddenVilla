using AutoMapper;
using Business.Mapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelAmenityRepository : IHotelAmenityRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public HotelAmenityRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<HotelAmenityDTO> CreateAmenity(HotelAmenityDTO amenityDTO)
        {
            HotelAmenity hotelAmenity = mapper.Map<HotelAmenityDTO, HotelAmenity>(amenityDTO);
            var addAmenity = await dbContext.Amenity.AddAsync(hotelAmenity);
            await dbContext.SaveChangesAsync();

            return mapper.Map<HotelAmenity, HotelAmenityDTO>(addAmenity.Entity);
        }

        public async Task<int> DeleteAmenityById(int amenityId)
        {
            var FindAmenity = await dbContext.Amenity.FindAsync(amenityId);
            if (FindAmenity != null)
            {
                dbContext.Amenity.Remove(FindAmenity);
                await dbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<HotelAmenityDTO>> GetAllAmenity()
        {
            try
            {
                IEnumerable<HotelAmenityDTO> hotelamenities =
                            mapper.Map<IEnumerable<HotelAmenity>, IEnumerable<HotelAmenityDTO>>(dbContext.Amenity);
                return hotelamenities;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<HotelAmenityDTO> GetAmenity(int amenityId)
        {
            try
            {
                HotelAmenityDTO hotelamenity = mapper.Map<HotelAmenity, HotelAmenityDTO>(
                    await dbContext.Amenity.FirstOrDefaultAsync(x => x.Id == amenityId));

                return hotelamenity;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<HotelAmenityDTO> IsUnique(string name, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    //create
                    HotelAmenityDTO hotelAmenity = mapper.Map<HotelAmenity, HotelAmenityDTO>(
                    await dbContext.Amenity.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));

                    return hotelAmenity;
                }
                else
                {
                    //update
                    HotelAmenityDTO hotelAmenity = mapper.Map<HotelAmenity, HotelAmenityDTO>(
                   await dbContext.Amenity.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != id));

                    return hotelAmenity;
                }
            }
            catch(Exception e) {
                return null;
            }
        }

        public async Task<HotelAmenityDTO> UpdateAmenity(int amenityId, HotelAmenityDTO amenityDTO)
        {
           try
            {
                if(amenityId == amenityDTO.Id)
                {
                    var Find = await dbContext.Amenity.FindAsync(amenityId);
                    var Amenity = mapper.Map<HotelAmenityDTO, HotelAmenity>(amenityDTO, Find);
                    var Update = dbContext.Amenity.Update(Amenity);
                    await dbContext.SaveChangesAsync();

                    return mapper.Map<HotelAmenity, HotelAmenityDTO>(Update.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
