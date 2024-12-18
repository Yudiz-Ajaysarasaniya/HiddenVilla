using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Roles
    {
        public const string Role_Admin = "Admin";

        public const string Role_Customer = "Customer";

        public const string Role_Employee = "Employee";
    }

    public static class LStorage
    {
        public const string localstorage = "RoomBookingInfo";
        public const string RoomOrder = "RoomOrders";
        public const string Local_Token = "JWT Token";
        public const string UserDetails = "User Details";
    }

    public class SetStatus
    {
        public const string Status_Pending = "Pending";
        public const string Status_Booked = "Booked";
        public const string Status_CheckedIn = "CheckedIn";
        public const string Status_CheckedOut_Complete = "CheckedOut";
        public const string Status_NoShow = "NoShow";
        public const string Status_Cancelled = "Cancelled";
    }
}
