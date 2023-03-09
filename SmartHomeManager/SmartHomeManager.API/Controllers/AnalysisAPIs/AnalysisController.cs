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
using SmartHomeManager.Domain.Common.Exceptions;
using SmartHomeManager.Domain.AnalysisDomain.DTOs;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;
using Microsoft.Identity.Client;
using SmartHomeManager.Domain.AccountDomain.Interfaces;

namespace SmartHomeManager.API.Controllers.AnalysisAPIs
{
    [Route("api/analysis")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        // TODO: Add private service variables
        private readonly ReportService _reportService;
        private readonly CarbonFootprintService _carbonFootprintService;
        private readonly ForecastService _forecastService;


        // TODO: Create constructor to inject services...
        public AnalysisController(IGenericRepository<CarbonFootprint> carbonFootprintRepository, IDeviceRepository deviceRepository, IForecastRepository forecastRepository, IAccountRepository accountRepository)
        {
            _reportService = new(deviceRepository);
            _carbonFootprintService = new(carbonFootprintRepository);
            _forecastService = new(forecastRepository, accountRepository);
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

        [HttpGet("/householdReport/energyUsageForecast/{accountId}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetHouseholdForecast(Guid accountId)
        {

            List<ForecastChartObjectDTO> getForecastChart = new List<ForecastChartObjectDTO>();

            // Use the service here...
            IEnumerable<ForecastChart> forecastCharts;

            try
            {
                forecastCharts = await _forecastService.GetHouseHoldForecast(accountId);
            }
            catch (AccountNotFoundException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            foreach (var forecastChart in forecastCharts)
            {
                getForecastChart.Add(new ForecastChartObjectDTO
                {
                    ForecastChartId = forecastChart.ForecastChartId,
                    AccountId = forecastChart.AccountId,
                    TimespanType = forecastChart.TimespanType,
                    DateOfAnalysis = forecastChart.DateOfAnalysis,
                });
            }

            return StatusCode(200, "Success");
        }

        [HttpGet("{accountId}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetHouseholdForecastData(Guid accountId)
        {

            List<ForecastChartDataObjectDTO> getForecastChart = new List<ForecastChartDataObjectDTO>();

            // Use the service here...
            IEnumerable<ForecastChartData> forecastChartDatas;

            try
            {
                forecastChartDatas = await _forecastService.GetHouseHoldForcastData(accountId);
            }
            catch (AccountNotFoundException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            foreach (var forecastChartData in forecastChartDatas)
            {
                getForecastChart.Add(new ForecastChartDataObjectDTO
                {
                    ForecastChartDataId = forecastChartData.ForecastChartDataId,
                    ForcastChartId = forecastChartData.ForecastChartId,
                    Label = forecastChartData.Label,
                    Value = forecastChartData.Value,
                    IsForecast = forecastChartData.IsForecast,
                    Index = forecastChartData.Index,
                });
            }

            return StatusCode(200, "Success");
        }
    }
}
