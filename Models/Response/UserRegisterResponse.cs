using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Response
{
    public class UserRegisterResponse
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}
