using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class StripePeymentDTO
    {
        public string ProductName { get; set; }
        public long Amount { get; set; }
        public List<string> ImageUrl { get; set; }
        public string ReturnUrl { get; set; }
    }
}
