using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VV.WebApp.MVC.Models;
using VV.WebApp.MVC.Services.Interfaces;

namespace VV.WebApp.MVC.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthenticationResponse> Login(UserLogin user)
        {
            StringContent stringContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:5000/api/auth/login", stringContent);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (!IsValidResponse(response))
            {
                return new AuthenticationResponse()
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<AuthenticationResponse>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<AuthenticationResponse> Register(UserRegister user)
        {
            StringContent stringContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:5000/api/auth/register", stringContent);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (!IsValidResponse(response))
            {
                return new AuthenticationResponse()
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<AuthenticationResponse>(await response.Content.ReadAsStringAsync(), options);
        }
    }
}
