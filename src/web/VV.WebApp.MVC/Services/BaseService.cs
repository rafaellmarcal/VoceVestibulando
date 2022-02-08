using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VV.WebApp.MVC.Exceptions;

namespace VV.WebApp.MVC.Services
{
    public abstract class BaseService
    {
        protected async Task<T> DeserializeHttpResponseMessage<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected bool IsValidResponse(HttpResponseMessage responseMessage)
        {
            switch ((int)responseMessage.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(responseMessage.StatusCode);

                case 400:
                    return false;
            }

            responseMessage.EnsureSuccessStatusCode();
            return true;
        }
    }
}
