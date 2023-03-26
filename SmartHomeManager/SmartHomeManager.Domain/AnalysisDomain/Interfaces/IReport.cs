using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.AnalysisDomain.Interfaces
{
    public interface IReport
    {
        public Task<PdfFile> GetDeviceReport(Guid deviceId, int lastMonths);
        public Task<PdfFile> GetHouseholdReport(Guid accountId, int lastMonths);

        public Task<IEnumerable<Device>?> GetDevicesByGUID(Guid accountId);
    }
}
