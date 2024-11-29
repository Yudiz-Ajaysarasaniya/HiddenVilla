using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<HotelRoomImage> Images { get; set; }
        public DbSet<HotelAmenity> Amenity{ get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<RoomOrderDetails> RoomOrderDetails { get; set; }
    }
}
