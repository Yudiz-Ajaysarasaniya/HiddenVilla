using Models.Request;
using Models.Response;

namespace HiddenVilla_Client.Service.IService
{
    public interface IAuthenticationService
    {
         Task<UserRegisterResponse> RegisterUser(UserRegistrationRequest request);
         Task<UserLoginResponse> LoginUser(UserLoginRequest request);
         Task LogoutUser();
    }
}
