using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using VV.WebApp.MVC.Exceptions;

namespace VV.WebApp.MVC.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomHttpRequestException exception)
            {
                HandlerRequestExceptionAsync(exception, context);
            }
        }

        private void HandlerRequestExceptionAsync(CustomHttpRequestException exception, HttpContext context)
        {
            if (exception.StatusCode == HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
                return;
            }

            context.Response.StatusCode = (int)exception.StatusCode;
        }
    }
}
