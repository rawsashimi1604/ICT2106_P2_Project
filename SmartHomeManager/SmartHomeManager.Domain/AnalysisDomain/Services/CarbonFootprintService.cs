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

        public string GetCarbonFootprintAsync(Guid accountId, int month, int year)
        {
            // Check if the data exist in database
            
            // Get all the usage data belonging to one accountId
            // Find which device belong to which account...


            // Sum it all up
            // Compare it to the national
            // Add to the database
            // Return to controller

            return "carbon footprint";
        }

    }
}
