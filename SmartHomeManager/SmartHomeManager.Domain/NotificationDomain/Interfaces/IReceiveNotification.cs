using System;
namespace SmartHomeManager.Domain.NotificationDomain.Interfaces
{
	public interface IReceiveNotification
	{
        public Task<IEnumerable<Entities.NotificationDomain>> GetAllNotificationsAsync();
        public Task<IEnumerable<Entities.NotificationDomain>?> GetNotificationsAsync(Guid accountId);

    }
}

