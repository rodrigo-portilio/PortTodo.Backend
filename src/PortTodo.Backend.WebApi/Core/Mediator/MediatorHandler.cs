using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using PortTodo.Backend.WebApi.Core.Messages;

namespace PortTodo.Backend.WebApi.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task PublishNotification<T>(T notification) where T : Notification
        {
            await _mediator.Publish(notification);
        }

        public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}