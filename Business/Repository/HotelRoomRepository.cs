using AutoMapper;
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
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public HotelRoomRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
                if (roomId == 0)
                {
                    //create
                    HotelRoomDTO hotelRoom = mapper.Map<HotelRoom, HotelRoomDTO>(
                    await dbContext.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower())); // Check if room already exist

                    return hotelRoom;
                }
                else
                {
                    //update
                    HotelRoomDTO hotelRoom = mapper.Map<HotelRoom, HotelRoomDTO>(
                    await dbContext.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != roomId)); // Check if room already exist

                    return hotelRoom;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom = mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
            hotelRoom.CreatedBy = "";
            var addRoom = await dbContext.HotelRooms.AddAsync(hotelRoom);
            await dbContext.SaveChangesAsync();

            return mapper.Map<HotelRoom,HotelRoomDTO>(addRoom.Entity);
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms(string checkInDatestr, string checkOutDatestr)
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs =
                            mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(dbContext.HotelRooms.Include(x => x.HotelRoomImages));
                return hotelRoomDTOs;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDatestr, string checkOutDatestr)
        {
            try
            {
                //Include(x => x.HotelRoomImages) this will load images that we define as foreign key
                //HotelRoom hotelRoom = await dbContext.HotelRooms.FirstOrDefaultAsync(x => x.Id == roomId);
                HotelRoomDTO hotelRoom = mapper.Map<HotelRoom, HotelRoomDTO>(
                    await dbContext.HotelRooms.Include(x => x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId)); 

                return hotelRoom;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if(roomId == hotelRoomDTO.Id)
                {
                    var roomDetails = await dbContext.HotelRooms.FindAsync(roomId);
                    var room = mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO,roomDetails);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var update = dbContext.HotelRooms.Update(room);
                    await dbContext.SaveChangesAsync();

                    return mapper.Map<HotelRoom, HotelRoomDTO>(update.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
            var findRoom = await dbContext.HotelRooms.FindAsync(roomId);
            if(findRoom != null)
            {
                var allImages = await dbContext.Images.Where(x => x.RoomId == roomId).ToListAsync(); // find images by roomid
                
                dbContext.Images.RemoveRange(allImages); // delete from db
                dbContext.HotelRooms.Remove(findRoom);
                return await dbContext.SaveChangesAsync(); 
            }
            return 0;
        }
    }
}
