using System.Net.Http;
using VV.WebApp.MVC.Models.Exceptions;

namespace VV.WebApp.MVC.Services
{
    public abstract class BaseService
    {

        protected bool ErrorsResponse(HttpResponseMessage responseMessage)
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
