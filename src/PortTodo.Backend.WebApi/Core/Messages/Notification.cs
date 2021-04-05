using System;
using MediatR;

namespace PortTodo.Backend.WebApi.Core.Messages
{
    public class Notification : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        public Notification()
        {
            Timestamp = DateTime.Now;
        }
    }
}