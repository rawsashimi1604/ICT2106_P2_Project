using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Services;
using SmartHomeManager.Domain.Common;
using Microsoft.AspNetCore.Hosting;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SmartHomeManager.Domain.AnalysisDomain.Services;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.API.Controllers.AnalysisAPIs
{
    [Route("api/analysis")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        // TODO: Add private service variables
        private readonly ReportService _reportService;
        private readonly CarbonFootprintService _carbonFootprintService;

        // TODO: Create constructor to inject services...
        public AnalysisController(IGenericRepository<CarbonFootprint> carbonFootprintRepository, IDeviceRepository deviceRepository)
        {
            _reportService = new(deviceRepository);
            _carbonFootprintService = new(carbonFootprintRepository);
        }

        // TODO: Create API Routes...

        // TODO: Device Route
        // GET /api/analysis/device/download/{deviceId}
        [HttpGet("device/download/")]
        public async Task<FileContentResult> GetDeviceReport()
        {
            PdfFile file = await _reportService.GetDeviceReport();
            return File(file.FileContents, file.ContentType, file.FileName);
        }

        // TODO: HouseholdReport Route
        // GET /api/analysis/householdReport/download/{accountId}

        // TODO: HouseholdEnergyUsageForecast Route
        // GET /api/analysis/householdReport/energyUsageForecast/{accountId}

        // TODO: HouseholdEnergyEfficiency Route
        // GET /api/analysis/householdReport/energyEfficiency/{accountId}

        // TODO: CarbonFootprint Route
        // GET /api/analysis/householdReport/carbonFootprint/{accountId}
        [HttpGet("/householdReport/carbonFootprint/{accountId}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetCarbonFootprintData(Guid accountId)
        {
            string result = _carbonFootprintService.GetCarbonFootprintAsync(Guid.NewGuid(), 1, 1);
            return StatusCode(200, result);
        }
    }
}
