using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.Common;

namespace SmartHomeManager.Domain.AnalysisDomain.Interfaces
{
    public interface ICarbonFootprintRepository : IGenericRepository<CarbonFootprint>
    {
        public Task<CarbonFootprint?> GetCarbonFootprintByMonthAndYear(int month, int year);
    }
}
