using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using VV.WebAPI.Core.Configuration;
using VV.WebApp.MVC.Extensions;
using VV.WebApp.MVC.Models;
using VV.WebApp.MVC.Services.Interfaces;

namespace VV.WebApp.MVC.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(
            HttpClient httpClient,
            IOptions<AppSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.AuthUrl);
            _httpClient = httpClient;
        }

        public async Task<AuthenticationResponse> Login(UserLogin user)
        {
            HttpResponseMessage response = await _httpClient.PostAsync("auth/login", user.ToStringContent());

            return await ProcessResponse(response);
        }

        public async Task<AuthenticationResponse> Register(UserRegister user)
        {
            HttpResponseMessage response = await _httpClient.PostAsync("auth/register", user.ToStringContent());

            return await ProcessResponse(response);
        }

        private async Task<AuthenticationResponse> ProcessResponse(HttpResponseMessage response)
        {
            if (!IsValidResponse(response))
            {
                return new AuthenticationResponse()
                {
                    ResponseResult = await DeserializeHttpResponseMessage<ResponseResult>(response)
                };
            }

            return await DeserializeHttpResponseMessage<AuthenticationResponse>(response);
        }
    }
}
