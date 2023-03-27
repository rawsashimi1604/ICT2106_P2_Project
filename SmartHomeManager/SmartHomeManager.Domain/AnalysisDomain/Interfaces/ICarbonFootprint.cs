using System;
using SmartHomeManager.Domain.AnalysisDomain.Entities;

namespace SmartHomeManager.Domain.AnalysisDomain.Interfaces
{
	public interface ICarbonFootprint
	{
        public Task<IEnumerable<CarbonFootprint>> GetCarbonFootprintAsync(Guid accountId, int month, int year);

    }
}

