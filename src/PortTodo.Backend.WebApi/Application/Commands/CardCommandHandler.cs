using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using PortTodo.Backend.Domain.Interfaces;
using PortTodo.Backend.Domain.Models;
using PortTodo.Backend.WebApi.Application.Notifications;
using PortTodo.Backend.WebApi.Core.Messages;

namespace PortTodo.Backend.WebApi.Application.Commands
{
    public class CardCommandHandler : CommandHandler, 
        IRequestHandler<CreateCardCommand, ValidationResult>
    {
        private readonly ICardRepository _cardRepository;


        public CardCommandHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        
        public async Task<ValidationResult> Handle(CreateCardCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                return message.ValidationResult;
            }

            Card card = new(message.Id, message.Name);

            _cardRepository.Add(card);
            
            card.AddNotification(new CardCreatedNotification(message.Id, message.Name));

            return await PersistData(_cardRepository.UnitOfWork);
        }
    }
}