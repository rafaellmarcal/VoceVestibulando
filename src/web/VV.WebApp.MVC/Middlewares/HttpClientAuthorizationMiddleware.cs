using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using VV.WebApp.MVC.Models.User;

namespace VV.WebApp.MVC.Middlewares
{
    public class HttpClientAuthorizationMiddleware : DelegatingHandler
    {
        private readonly IUserAuthenticated _userAuthenticated;

        public HttpClientAuthorizationMiddleware(IUserAuthenticated userAuthenticated)
        {
            _userAuthenticated = userAuthenticated;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _userAuthenticated.ObterHttpContext().Request.Headers["Authorization"];

            if (!string.IsNullOrWhiteSpace(authorizationHeader))
                request.Headers.Add("Authorization", new List<string>() { authorizationHeader });

            var token = _userAuthenticated.ObterUserToken();

            if (token != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
