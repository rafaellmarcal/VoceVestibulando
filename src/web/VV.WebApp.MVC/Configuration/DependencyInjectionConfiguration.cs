using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using VV.WebApp.MVC.Middlewares;
using VV.WebApp.MVC.Models.User;
using VV.WebApp.MVC.Services;
using VV.WebApp.MVC.Services.Interfaces;

namespace VV.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationMiddleware>();

            services.AddHttpClient<IAuthService, AuthService>();

            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationMiddleware>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUserAuthenticated, UserAuthenticated>();

            return services;
        }
    }
}
