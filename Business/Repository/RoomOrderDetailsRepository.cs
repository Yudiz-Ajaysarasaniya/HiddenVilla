﻿using AutoMapper;
using Business.Repository.IRepository;
using Common;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class RoomOrderDetailsRepository : IRoomOrderDetailsRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public RoomOrderDetailsRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details)
        {
            try
            {
                details.CheckInDate = details.CheckInDate.Date;
                details.CheckOutDate = details.CheckOutDate.Date;
                var roomOrder = mapper.Map<RoomOrderDetailsDTO, RoomOrderDetails>(details);
                roomOrder.Status = SetStatus.Status_Pending;
                //roomOrder.HotelRoom.HotelRoomImages = details.HotelRoomDTO.HotelRoomImages;
                var result = await dbContext.RoomOrderDetails.AddAsync(roomOrder);
                await dbContext.SaveChangesAsync();

                return mapper.Map<RoomOrderDetails, RoomOrderDetailsDTO>(result.Entity);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<RoomOrderDetailsDTO>> GetAllRoomOrderDetails()
        {
            try
            {
                IEnumerable<RoomOrderDetailsDTO> roomOrders = mapper.Map<IEnumerable<RoomOrderDetails>, IEnumerable<RoomOrderDetailsDTO>>(dbContext.RoomOrderDetails.Include(x => x.HotelRoom));
                return roomOrders;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RoomOrderDetailsDTO> GetRoomOrderDetails(int roomOrderId)
        {
            try
            {
                RoomOrderDetails roomOrder = await dbContext.RoomOrderDetails
                    .Include(u => u.HotelRoom).ThenInclude(x => x.HotelRoomImages)
                    .FirstOrDefaultAsync(x => x.Id == roomOrderId);

                RoomOrderDetailsDTO orderDetailsDTO = mapper.Map<RoomOrderDetails, RoomOrderDetailsDTO>(roomOrder);

                orderDetailsDTO.HotelRoomDTO.TotalDays = orderDetailsDTO.CheckOutDate.Subtract(orderDetailsDTO.CheckInDate).Days;

                return orderDetailsDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            var status = false;
            var existingBooking = await dbContext.RoomOrderDetails.Where(x => x.RoomId == roomId && x.IsPaymentSuccess &&
            (checkInDate < x.CheckInDate.Date && checkInDate.Date > x.CheckInDate ||
            checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date < x.CheckInDate.Date)).FirstOrDefaultAsync();

            if (existingBooking != null)
            {
                status = true;
            }
            return status;
        }

        public Task<RoomOrderDetailsDTO> PaymentStatus(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderStatus(int roomOrderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}