using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class HotelAmenityDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Amenity Name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please Enter Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please Enter Time")]
        public string? Timing { get; set; }
        public string? Icon { get; set; }
    }
}
