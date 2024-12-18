using AutoMapper;
using DataAccess.Data.Entities;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()     
        {
            CreateMap<HotelRoomDTO, HotelRoom>();
            CreateMap<HotelRoom, HotelRoomDTO>();
            CreateMap<HotelRoomImageDTO, HotelRoomImage>().ReverseMap(); // two-way mapping
            //CreateMap<HotelRoomImage, HotelRoomImageDTO>();
            CreateMap<HotelAmenityDTO, HotelAmenity>().ReverseMap();

            CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>().ForMember(x => x.HotelRoomDTO, opt => opt.MapFrom(y => y.HotelRoom));
            CreateMap<RoomOrderDetailsDTO, RoomOrderDetails>();
        }
    }
}
