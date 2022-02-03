using Microsoft.Extensions.DependencyInjection;
using VV.Catalogo.API.Data;

namespace VV.Catalogo.API.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<CatalogoDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            return services;
        }
    }
}
