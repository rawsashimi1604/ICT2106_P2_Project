using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Services;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.Common.Exceptions;
using SmartHomeManager.Domain.AnalysisDomain.DTOs;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;
using Microsoft.Identity.Client;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.DTOs;
using SmartHomeManager.Domain.Common.DTOs;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Interfaces;

namespace SmartHomeManager.API.Controllers.AnalysisAPIs
{
    [Route("api/analysis")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        // TODO: Add private service variables
        private readonly IReport _reportService;
        private readonly IForecast _forecastService;
        private readonly ICarbonFootprint _carbonFootprintService;
        private readonly AbstractDTOFactory _dtoFactory;
        private readonly IEnergyEfficiency _energyEfficiencyService;
        

        public AnalysisController(
            IReport report,
            IForecast forecast,
            ICarbonFootprint carbonFootprint,
            IEnergyEfficiency energyEfficiency,
            IEnergyEfficiencyRepository energyEfficiencyRepository
        )
        {
            _reportService = report;
            _carbonFootprintService = carbonFootprint;
            _energyEfficiencyService = energyEfficiency;
            _forecastService = forecast;
            _dtoFactory = new AnalysisDTOFactory();
        }

        // GET /api/analysis/device/download/{deviceId}
        [HttpGet("device/download/{deviceId}/{lastMonths}")]
        public async Task<IActionResult> GetDeviceReport(Guid deviceId, int lastMonths)
        {
            PdfFile file;
            try
            {
                file = await _reportService.GetDeviceReport(deviceId, lastMonths);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(
                    400,
                    _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANALYSIS_REPORTDEVICE,
                        null,
                        400,
                        ex.Message
                    )
                );
            }
            catch (DeviceNotFoundException ex)
            {
                return StatusCode(
                    400,
                    _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANALYSIS_REPORTDEVICE,
                        null,
                        400,
                        ex.Message
                    )
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANALYSIS_REPORTDEVICE,
                        null,
                        500,
                        ex.Message
                    )
                );
            }

            return (FileContentResult) File(file.FileContents, file.ContentType, file.FileName);
        }

        // GET /api/analysis/householdReport/download/{accountId}
        [HttpGet("householdReport/download/{accountId}/{lastMonths}")]
        public async Task<IActionResult> GetHouseholdReport(Guid accountId, int lastMonths)
        {

            PdfFile file;

            try
            {
                file = await _reportService.GetHouseholdReport(accountId, lastMonths);

            }
            catch (ArgumentException ex)
            {
                return StatusCode(
                    400,
                    _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANALYSIS_REPORTHOUSEHOLD,
                        null,
                        400,
                        ex.Message
                    )
                );
            }
            catch (AccountNotFoundException ex)
            {
                return StatusCode(
                    400,
                    _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANALYSIS_REPORTHOUSEHOLD,
                        null,
                        400,
                        ex.Message
                    )
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANALYSIS_REPORTHOUSEHOLD,
                        null,
                        500,
                        ex.Message
                    )
                );
            }

            return (FileContentResult)File(file.FileContents, file.ContentType, file.FileName);

        }

        // GET /api/analysis/device/{accountId}
        [HttpGet("device/{accountId}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetDevicesByGUID(Guid accountId)
        {
            // Use the service here...
            IEnumerable<Device> devices = new List<Device>();

            try
            {
                devices = await _reportService.GetDevicesByGUID(accountId);
            }
            catch(AccountNotFoundException ex)
            {
                return StatusCode(
                    400,
                    _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.DEVICE_GETBYACCOUNT, devices, 400, ex.Message
                    )
                );
            }
            catch(Exception ex)
            {
                return StatusCode(
                    500,
                    _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.DEVICE_GETBYACCOUNT, devices, 400, ex.Message
                    )
                );
            }

            return StatusCode(200, _dtoFactory.CreateResponseDTO
                (ResponseDTOType.DEVICE_GETBYACCOUNT, devices, 200, "Success"));
        }

        // GET /api/analysis/householdReport/energyUsageForecast/{accountId}


        // GET /api/analysis/householdReport/energyEfficiency/{accountId}
        [HttpGet("householdReport/energyEfficiency/{accountId}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetHouseholdEnergyEfficiency(Guid accountId)
        {
            IEnumerable<EnergyEfficiency> allEnergyEfficiency = new List<EnergyEfficiency>();

            try
            {
                allEnergyEfficiency = await _energyEfficiencyService.GetAllDeviceEnergyEfficiency(accountId);
            }
            catch (DBReadFailException ex)
            {
                return StatusCode(
                400,
                _dtoFactory.CreateResponseDTO(
                    ResponseDTOType.ANALYSIS_ENERGYEFFICIENCY_GETALL,
                    allEnergyEfficiency,
                    400,
                    ex.Message)
                );
            }
            catch (AccountNotFoundException ex)
            {
                return StatusCode(
                400,
                _dtoFactory.CreateResponseDTO(
                    ResponseDTOType.ANALYSIS_ENERGYEFFICIENCY_GETALL,
                    allEnergyEfficiency,
                    400,
                    ex.Message)
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                500,
                _dtoFactory.CreateResponseDTO(
                    ResponseDTOType.ANALYSIS_ENERGYEFFICIENCY_GETALL,
                    allEnergyEfficiency,
                    500,
                    ex.Message)
                );
            }
            if (allEnergyEfficiency != null) {
            }
            return StatusCode(
                200,
                _dtoFactory.CreateResponseDTO(
                    ResponseDTOType.ANALYSIS_ENERGYEFFICIENCY_GETALL,
                    allEnergyEfficiency,
                    200,
                    "Success")
                );
        }

        // GET /api/analysis/householdReport/carbonFootprint/{accountId}
        [HttpGet("householdReport/carbonFootprint/{accountId}/{year}-{month}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetCarbonFootprintData(Guid accountId, int month, int year)
        {
            IEnumerable<CarbonFootprint> result = new List<CarbonFootprint>();

            try
            {
                result = await _carbonFootprintService.GetCarbonFootprintAsync(accountId, month, year);
            }
            catch (AccountNotFoundException ex)
            {
                return StatusCode(
                    400,
                    _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANALYSIS_CARBONFOOTPRINT_GETBYACCOUNTMONTHYEAR,
                        result,
                        400,
                        ex.Message
                    ));
            }
            catch (InvalidDateInputException ex)
            {
                return StatusCode(
                    400,
                    _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANALYSIS_CARBONFOOTPRINT_GETBYACCOUNTMONTHYEAR,
                        result,
                        400,
                        ex.Message
                    ));
            }
            catch (NoCarbonFootprintDataException ex)
            {
                return StatusCode(
                    500,
                    _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANALYSIS_CARBONFOOTPRINT_GETBYACCOUNTMONTHYEAR,
                        result,
                        500,
                        ex.Message
                    ));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANALYSIS_CARBONFOOTPRINT_GETBYACCOUNTMONTHYEAR,
                        result,
                        500,
                        ex.Message
                    ));
            }

            return StatusCode(
                200,
                _dtoFactory.CreateResponseDTO(
                    ResponseDTOType.ANALYSIS_CARBONFOOTPRINT_GETBYACCOUNTMONTHYEAR,
                    result,
                    200,
                    "Success"
                )
            );
        }

        // GET /api/analysis/householdReport/energyUsageForecast/{accountId}/{timespan}
        [HttpGet("householdReport/energyUsageForecast/{accountId}/{timespan}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetHouseholdForecast(Guid accountId, int timespan)
        {
            
            // Use the service here...
            IEnumerable<ForecastChartData> forecastChartDatas = new List<ForecastChartData>();

            try
            {
                forecastChartDatas = await _forecastService.GetHouseHoldForecast(accountId, timespan);

                return StatusCode(
                    200,
                    _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANAYLSIS_FORECAST_GETBYACCOUNTTIMESPAN,
                        forecastChartDatas,
                        200,
                        "Success"
                    )
                );

            }
            catch (AccountNotFoundException ex)
            {
                return StatusCode(400, _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANAYLSIS_FORECAST_GETBYACCOUNTTIMESPAN,
                        forecastChartDatas,
                        400,
                        ex.Message
                    ));
            }
            catch (ArgumentException ex)
            {
                return StatusCode(400, _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANAYLSIS_FORECAST_GETBYACCOUNTTIMESPAN,
                        forecastChartDatas,
                        400,
                        ex.Message
                    ));
            }
            catch (Exception ex)
            {
                return StatusCode(400, _dtoFactory.CreateResponseDTO(
                        ResponseDTOType.ANAYLSIS_FORECAST_GETBYACCOUNTTIMESPAN,
                        forecastChartDatas,
                        500,
                        ex.Message
                    ));
            }

        }
    }
}
