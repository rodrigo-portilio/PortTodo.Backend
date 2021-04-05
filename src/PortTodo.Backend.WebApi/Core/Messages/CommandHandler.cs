using System.Threading.Tasks;
using FluentValidation.Results;
using PortTodo.Backend.WebApi.Models.Interfaces;

namespace PortTodo.Backend.WebApi.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
        {
            if (!await uow.Commit())
            {
                AddError("There was an error at moment in data persist!");
            }

            return ValidationResult;
        }
    }
}