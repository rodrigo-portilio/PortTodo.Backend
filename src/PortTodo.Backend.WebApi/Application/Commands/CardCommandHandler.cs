using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using PortTodo.Backend.WebApi.Application.Notifications;
using PortTodo.Backend.WebApi.Core.Messages;
using PortTodo.Backend.WebApi.Models;
using PortTodo.Backend.WebApi.Models.Interfaces;

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