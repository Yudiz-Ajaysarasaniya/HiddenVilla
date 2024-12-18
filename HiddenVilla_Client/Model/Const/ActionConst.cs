namespace HiddenVilla_Client.Model.Const
{
    public class ActionConst
    {
        private const string BaseRoute = "https://localhost:7158/api";

        public const string GetAllRooms = BaseRoute + "/hotelroom/get_list";
        public const string GetAllAmenity = BaseRoute + "/hotelamenity/get_list";

        public const string Payment = BaseRoute + "/payment/checkout";
        public const string SaveOrder = BaseRoute + "/roomorder/create_order";
        public const string PaymentSuccess = BaseRoute + "/roomorder/paymentsuccess";
        public const string Register = BaseRoute + "/account/signup";
        public const string Login = BaseRoute + "/account/login";
    }
}

