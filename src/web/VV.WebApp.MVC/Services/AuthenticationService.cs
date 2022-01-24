using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VV.WebApp.MVC.Models;

namespace VV.WebApp.MVC.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserResponseLogin> Login(UserLogin user)
        {
            StringContent stringContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("", stringContent);

            return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync());
        }

        public async Task<UserResponseLogin> Register(UserRegister user)
        {
            StringContent stringContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("", stringContent);

            return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync());
        }
    }
}
