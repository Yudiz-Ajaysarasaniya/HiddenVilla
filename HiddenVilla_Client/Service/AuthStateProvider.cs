using Blazored.LocalStorage;
using Common;
using HiddenVilla_Client.Helper;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace HiddenVilla_Client.Service
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        public AuthStateProvider( HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
           var token = await localStorageService.GetItemAsync<string>(LStorage.Local_Token);
            if (token == null) return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
        }

        public void NotifyUserLoggedIn(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }
        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
