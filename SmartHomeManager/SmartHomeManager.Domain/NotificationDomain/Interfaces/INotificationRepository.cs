﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;

namespace SmartHomeManager.Domain.NotificationDomain.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        public Task<IEnumerable<Notification>> GetAllByIdAsync(Guid id);
    }
}
