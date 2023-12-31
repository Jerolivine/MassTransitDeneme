﻿using Microsoft.OpenApi.Models;

namespace MassTransitDeneme.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwagger(this WebApplicationBuilder builder)
        {
            // Add services required for Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Memorize Words API", Version = "v1" });
            });
        }

        public static void ConfigureSwagger(this WebApplication app)
        {
            // Configure Swagger and the Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Memorize Words V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
