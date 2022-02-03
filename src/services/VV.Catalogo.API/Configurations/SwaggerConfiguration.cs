﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace VV.Catalogo.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "VocêVestibulando - Catalog API",
                    Description = "API de catalogo dos cursos do VocêVestibulando.",
                    Contact = new OpenApiContact() { Name = "Rafael Marçal", Email = "rafaell.lima92@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }
    }
}
