using System;
using PortTodo.Backend.WebApi.Models.Interfaces;

namespace PortTodo.Backend.WebApi.Models
{
    public enum CardStatus
    {
        ToDo, InProgress, Done
    }
    public class Card : Entity, IAggregateRoot
    {
        
        public string Name { get; private set; }
        public CardStatus Status { get; private set; }

        protected Card() {}

        public Card(Guid id, string name)
        {
            Id = id;
            Name = name;
            Status = CardStatus.ToDo;

            IsValid();
        }

        public override bool IsValid()
        {
            if(Id == Guid.Empty)
                throw new Exception("Id can't be empty");

            
            if (string.IsNullOrEmpty(Name) || Name.Length < 3)
                throw new Exception("Name can't be empty or less 3 character");

            return true;
        }

        public void ChangeStatus(CardStatus status)
        {
            Status = status;
        }

    }
}