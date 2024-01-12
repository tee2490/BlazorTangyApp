using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Tangy_Client.Service.IService;
using Tangy_Common;
using Tangy_Models;

namespace Tangy_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<SignInResponseDTO> Login(SignInRequestDTO signInRequest)
        {
            //แปลงจากคลาสเป็นรูปแบบ Json
            var content = JsonConvert.SerializeObject(signInRequest);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            //ส่งไปยังฝั่ง API
            var response = await _client.PostAsync("api/account/signin", bodyContent);

            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignInResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(SD.Local_Token, result.Token);
                await _localStorage.SetItemAsync(SD.Local_UserDetails, result.UserDTO);

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

                return new SignInResponseDTO() { IsAuthSuccessful = true };
            }
            else
            {
                return result;
            }
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO signUpRequest)
        {
            throw new NotImplementedException();
        }
    }
}
