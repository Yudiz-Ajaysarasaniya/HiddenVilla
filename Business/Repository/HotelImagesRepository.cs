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
using static System.Net.Mime.MediaTypeNames;

namespace Business.Repository
{
    public class HotelImagesRepository : IHotelImagesRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public HotelImagesRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<int> CreateRoomImage(HotelRoomImageDTO imageDTO)
        {
            var image = mapper.Map<HotelRoomImageDTO, HotelRoomImage>(imageDTO);
            var AddImage = await dbContext.Images.AddAsync(image);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteRoomImageByImageId(int imageId)
        {
            var FindImage = await dbContext.Images.FindAsync(imageId);
            dbContext.Images.Remove(FindImage);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteRoomImageByImageUrl(string imageUrl)
        {
            var allImages = await dbContext.Images.FirstOrDefaultAsync(x => x.RoomImageUrl.ToLower() == imageUrl.ToLower());
            if(allImages == null)
            {
                return 0;
            }
            dbContext.Images.Remove(allImages);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteRoomImageByRoomId(int roomId)
        {
            var ImageList = await dbContext.Images.Where(x => x.RoomId == roomId).ToListAsync();
            dbContext.Images.RemoveRange(ImageList);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId)
        {
            return mapper.Map<IEnumerable<HotelRoomImage>, IEnumerable<HotelRoomImageDTO>>(
                await dbContext.Images.Where(x => x.RoomId == roomId).ToListAsync());
        }
    }
}
