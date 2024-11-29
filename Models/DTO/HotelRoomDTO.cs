using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class HotelRoomDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Room Name")] //for client side validation
        public string? Name { get; set; }
        [Range(1, 5, ErrorMessage = "Occupancy must be between 1 and 5")]
        public int Occupancy { get; set; }
        [Range(1,3000, ErrorMessage = "Ragular Rate must be between 1 and 3000")]
        [Required]
        public double RagularRate { get; set; }
       // [Required(ErrorMessage = "Please Enter Details")]
        public string? Details { get; set; }
        public string? SqFt { get; set; }
        public double TotalDays { get; set; }
        public double TotalAmmount { get; set; }
        public bool IsBooked { get; set; }
        // to hold sollection of images
        public virtual ICollection<HotelRoomImageDTO>? HotelRoomImages { get; set; } // for virtual property we do not need to add migration
        //this will just hold images url
        public List<string> ImageUrls { get; set; }
    }
}
