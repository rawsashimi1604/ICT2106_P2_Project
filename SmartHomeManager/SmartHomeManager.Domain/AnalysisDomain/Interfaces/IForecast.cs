using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AnalysisDomain.Entities;

namespace SmartHomeManager.Domain.AnalysisDomain.Interfaces
{
    public interface IForecast
    {
        public Task<IEnumerable<ForecastChartData>> GetHouseHoldForecast(Guid accountId, int timespan);
    }
}
