using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AnalysisDomain.Interfaces
{
    public interface IForecastDataRepository : IGenericRepository<ForecastChartData>
    {
        public Task<IEnumerable<ForecastChartData>> GetAllByIdAsync(Guid id);
    }
}
