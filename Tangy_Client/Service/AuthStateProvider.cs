using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using Tangy_Client.Helper;
using Tangy_Common;

namespace Tangy_Client.Service
{
    //AuthenticationStateProvider เป็นคลาสที่ให้บริการเกี่ยวกับสถานะการยันยันตัวตัว
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(SD.Local_Token);
           
            if (token == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            //แนบโทเคนไว้ที่ส่วนหัวของ Client เพื่อใช้ติดต่อกับฝั่ง API
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            //แปลงจาก Token ไปเป็นค่าที่อ่านเข้าใจ ประกอบไปด้วย key กับ value
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
        }
    }
}
