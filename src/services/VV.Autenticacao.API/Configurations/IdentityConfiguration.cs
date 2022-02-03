using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VV.Autenticacao.API.Context;
using VV.Autenticacao.API.Extensions;

namespace VV.Autenticacao.API.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AutenticacaoDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AutenticacaoDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<IdentityMessagesLanguagePTBR>();

            return services;
        }
    }
}
