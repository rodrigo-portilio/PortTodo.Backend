using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PortTodo.Backend.Domain.Interfaces;
using PortTodo.Backend.Infra.Data;
using PortTodo.Backend.Infra.Data.Repositories;
using PortTodo.Backend.WebApi.Application.Commands;
using PortTodo.Backend.WebApi.Application.Queries;
using PortTodo.Backend.WebApi.Application.Services;
using PortTodo.Backend.WebApi.Core.Mediator;

namespace PortTodo.Backend.WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<CreateCardCommand, ValidationResult>, CardCommandHandler>();
            
            services.AddScoped<ICardQueries, CardQueries>();

            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<TodoContext>();
            
            services.AddScoped<ICacheService, CacheService>();
        }
    }
}