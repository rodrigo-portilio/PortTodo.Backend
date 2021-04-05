using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PortTodo.Backend.WebApi.Application.Queries;
using PortTodo.Backend.WebApi.Application.Services;

namespace PortTodo.Backend.WebApi.Application.Notifications
{
    public class CardNotificationHandler : INotificationHandler<CardCreatedNotification>
    {
        private readonly ICardQueries _cardQueries;
        private readonly ICacheService _cacheService;

        public CardNotificationHandler(ICardQueries cardQueries, ICacheService cacheService)
        {
            _cardQueries = cardQueries;
            _cacheService = cacheService;
        }
        
        public Task Handle(CardCreatedNotification notification, CancellationToken cancellationToken)
        {
            _cacheService.ClearCache("cards");

            return Task.CompletedTask;
        }
    }
}