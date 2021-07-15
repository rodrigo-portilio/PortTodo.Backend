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
            services.AddDbContext<TodoContext>(options => 
            {
                options.UseNpgsql(configuration.GetSection("POSTGRESQL_CONNECTION").Value);
            });

            services.AddStackExchangeRedisCache(options => 
            {
                options.Configuration = configuration.GetSection("REDIS_CONNECTION").Value;
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