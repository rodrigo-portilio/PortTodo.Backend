using System.Threading.Tasks;
using FluentValidation.Results;
using PortTodo.Backend.WebApi.Core.Messages;

namespace PortTodo.Backend.WebApi.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishNotification<T>(T notification) where T : Notification;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}