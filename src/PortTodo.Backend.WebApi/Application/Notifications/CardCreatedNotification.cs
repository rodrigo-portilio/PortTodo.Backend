using System;
using PortTodo.Backend.WebApi.Core.Messages;

namespace PortTodo.Backend.WebApi.Application.Notifications
{
    public class CardCreatedNotification : Notification
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        
        public CardCreatedNotification(Guid id, string name)
        {
            AggregateId = id;
            Id = id;
            Name = name;
        }
    }
}