using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.Common;

namespace SmartHomeManager.Domain.AnalysisDomain.Services
{
    public class CarbonFootprintService
    {

        private readonly IGenericRepository<CarbonFootprint> _carbonFootprintRepository;

        public CarbonFootprintService(IGenericRepository<CarbonFootprint> carbonFootprintRepository)
        {
            _carbonFootprintRepository = carbonFootprintRepository;
        }

        public string GetCarbonFootprintAsync(Guid accountId)
        {
            return "carbon footprint";
        }

    }
}
