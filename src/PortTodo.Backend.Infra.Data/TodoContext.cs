using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PortTodo.Backend.Core.Data;
using PortTodo.Backend.Domain.Models;
using PortTodo.Backend.WebApi.Core.Mediator;
using PortTodo.Backend.WebApi.Core.Messages;

namespace PortTodo.Backend.Infra.Data
{
    public sealed class TodoContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        
        public TodoContext(DbContextOptions<TodoContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Card> Cards { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Notification>();
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;

            if (success)
            {
                await _mediatorHandler.PublishNotifications(this).ConfigureAwait(false);
            }
            return success;
        }
    }
    
    public static class MediatorExtension
    {
        public static async Task PublishNotifications<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainNotifications = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearNotifications());

            var tasks = domainNotifications
                .Select(async (domainEvent) => {
                    await mediator.PublishNotification(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
    
}