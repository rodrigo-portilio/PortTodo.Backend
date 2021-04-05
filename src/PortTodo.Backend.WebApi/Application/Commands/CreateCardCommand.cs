using System;
using FluentValidation;
using PortTodo.Backend.WebApi.Core.Messages;

namespace PortTodo.Backend.WebApi.Application.Commands
{
    public class CreateCardCommand : Command
    {
        
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public CreateCardCommand(Guid id, string name)
        {
            if (id == Guid.Empty)
            {
                id = Guid.NewGuid();
            }
            AggregateId = id;
            Id = id;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateCardValidation().Validate(this);
            return ValidationResult.IsValid;
        }


        private class CreateCardValidation : AbstractValidator<CreateCardCommand>
        {
            public CreateCardValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id invalid");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .WithMessage("Name can't be empty or less 3 character");
            }
        }
        
        
    }
}