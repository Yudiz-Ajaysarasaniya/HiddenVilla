using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Response
{
    public class UserLoginResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public User user { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
