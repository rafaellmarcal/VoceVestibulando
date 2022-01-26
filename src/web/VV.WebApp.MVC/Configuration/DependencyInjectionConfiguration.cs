using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using VV.WebApp.MVC.Models.User;
using VV.WebApp.MVC.Services;

namespace VV.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUserAuthenticated, UserAuthenticated>();

            return services;
        }
    }
}
