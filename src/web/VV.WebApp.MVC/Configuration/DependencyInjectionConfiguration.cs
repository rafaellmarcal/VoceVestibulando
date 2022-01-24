using Microsoft.Extensions.DependencyInjection;
using VV.WebApp.MVC.Services;

namespace VV.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
