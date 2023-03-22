using System;
using SmartHomeManager.Domain.NotificationDomain.Entities;

namespace SmartHomeManager.Domain.NotificationDomain.Interfaces
{
	public interface IReceiveNotification
	{
        public Task<IEnumerable<Notification>> GetAllNotificationsAsync();
        public Task<IEnumerable<Notification>?> GetNotificationsAsync(Guid accountId);

    }
}

