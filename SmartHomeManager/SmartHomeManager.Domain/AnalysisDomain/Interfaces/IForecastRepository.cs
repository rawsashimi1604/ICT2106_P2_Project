using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AnalysisDomain.Interfaces
{
    public interface IForecastRepository : IGenericRepository<ForecastChart>
    {
        public Task<IEnumerable<NotificationDomain.Entities.Notification>> GetAllByIdAsync(Guid id);
    }
}
