using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PortTodo.Backend.Infra.Data;

namespace PortTodo.Backend.WebApi.Configuration
{
    public static class WebApiConfig
    {
        public static void AddWebApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var postgresql_connection = Environment.GetEnvironmentVariable("POSTGRESQL_CONNECTION");
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("POSTGRESQL_CONNECTION")))
            {
                postgresql_connection = "Host=localhost;Port=5432;Pooling=true;Database=Todo;User Id=postgres;Password=Aa@123456789;";
            }

            services.AddDbContext<TodoContext>(options => 
            {
                options.UseNpgsql(postgresql_connection);
            });

            services.AddStackExchangeRedisCache(options => 
            {
                options.Configuration = Environment.GetEnvironmentVariable("REDIS_CONNECTION");
            });

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });
        }

        public static void UseWebApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("Total");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}