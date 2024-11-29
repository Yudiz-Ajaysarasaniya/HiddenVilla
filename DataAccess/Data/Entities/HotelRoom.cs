using DataAccess.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities
{
    public class HotelRoom : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public double RagularRate { get; set; }
        public string? Details { get; set; }
        public string? SqFt { get; set; }
        public string? CreatedBy { get; set; }
        // updated by
        public string? UpdatedBy { get; set; }
        //to hold collection of data
        public virtual ICollection<HotelRoomImage> HotelRoomImages { get; set; } // for virtual property we do not need to add migration
    }
}
