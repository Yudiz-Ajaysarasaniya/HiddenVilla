using Blazored.LocalStorage;
using Common;
using HiddenVilla_Client.Model.Const;
using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Models.Request;
using Models.Response;
using Models.Response.Base;
using Newtonsoft.Json;
using System.Text;

namespace HiddenVilla_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationState;

        public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationState)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationState = authenticationState;
        }

        public async Task<UserLoginResponse> LoginUser(UserLoginRequest request)
        {
            var content = JsonConvert.SerializeObject(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(ActionConst.Login, bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserLoginResponse>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await localStorage.SetItemAsync(LStorage.Local_Token, result.Token);
                await localStorage.SetItemAsync(LStorage.UserDetails, result.user);
                ((AuthStateProvider)authenticationState).NotifyUserLoggedIn(result.Token);
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", result.Token);
                return new UserLoginResponse { IsSuccess = true };
            }
            else
            {
                return result;
            }
        }

        public async Task LogoutUser()
        {
            await localStorage.RemoveItemAsync(LStorage.Local_Token);
            await localStorage.RemoveItemAsync(LStorage.UserDetails);
            ((AuthStateProvider) authenticationState).NotifyUserLogout();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<UserRegisterResponse> RegisterUser(UserRegistrationRequest request)
        {
            var content = JsonConvert.SerializeObject(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(ActionConst.Register, bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserRegisterResponse>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new UserRegisterResponse { IsSuccess = true };
            }
            else
            {
                return result;
            }
        }
    }
}
