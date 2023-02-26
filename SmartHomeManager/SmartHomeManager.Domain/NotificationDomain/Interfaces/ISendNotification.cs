using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.NotificationDomain.Interfaces
{
    public interface ISendNotification
    {
        public Task<Tuple<NotificationResult, Notification?>> SendNotification(string notificationMessage, Guid accountId);

    }
}
