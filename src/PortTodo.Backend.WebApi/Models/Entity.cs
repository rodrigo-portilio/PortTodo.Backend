using System;
using System.Collections.Generic;
using PortTodo.Backend.WebApi.Core.Messages;

namespace PortTodo.Backend.WebApi.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }

        private List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications?.AsReadOnly();

        public void AddNotification(Notification notification)
        {
            _notifications ??= new List<Notification>();
            _notifications.Add(notification);
        }

        public void RemoveNotification(Notification notificationItem)
        {
            _notifications?.Remove(notificationItem);
        }

        public void ClearNotifications()
        {
            _notifications?.Clear();
        }
    }
}