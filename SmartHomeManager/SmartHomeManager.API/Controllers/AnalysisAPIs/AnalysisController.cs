using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Services;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.AnalysisDomain.Services;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.DTOs;
using SmartHomeManager.Domain.Common.DTOs;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;

namespace SmartHomeManager.API.Controllers.AnalysisAPIs
{
    [Route("api/analysis")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        // TODO: Add private service variables
        private readonly ReportService _reportService;
        private readonly ICarbonFootprint _carbonFootprintService;
        private readonly AbstractDTOFactory _dtoFactory;
    

        // TODO: Create constructor to inject services...
        public AnalysisController(
            IDeviceRepository deviceRepository, 
            ICarbonFootprint carbonFootprint

        ) 
        {
            _reportService = new(deviceRepository);
            _carbonFootprintService = carbonFootprint;
          //  _carbonFootprintService = new (carbonFootprintRepository, deviceLogRepository, accountRepository, deviceRepository);
            _dtoFactory = new AnalysisDTOFactory();
        }

        // TODO: Create API Routes...

        // TODO: Device Route
        // GET /api/analysis/device/download/{deviceId}
        [HttpGet("device/download/{deviceId}")]
        public async Task<FileContentResult> GetDeviceReport(Guid deviceId)
        {
            PdfFile file = await _reportService.GetDeviceReport(deviceId);
            return File(file.FileContents, file.ContentType, file.FileName);
        }


        // TODO: GET /api/device/{accountId}
        [HttpGet("device/{accountId}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetDevicesByGUID(Guid accountId)
        {
            // Use the service here...
            IEnumerable<Device> devices;

            devices = await _reportService.GetDevicesByGUID(accountId);

            return StatusCode(200, _dtoFactory.CreateResponseDTO
                (ResponseDTOType.DEVICE_GETBYACCOUNT, devices, 200, "Success"));
        }

        // TODO: HouseholdReport Route
        // GET /api/analysis/householdReport/download/{accountId}
        [HttpGet("householdReport/download")]
        public async Task<FileContentResult> GetHouseholdReport()
        {
            PdfFile file = await _reportService.GetHouseholdReport();
            return File(file.FileContents, file.ContentType, file.FileName);
        }

        // TODO: HouseholdEnergyUsageForecast Route
        // GET /api/analysis/householdReport/energyUsageForecast/{accountId}

        // TODO: HouseholdEnergyEfficiency Route
        // GET /api/analysis/householdReport/energyEfficiency/{accountId}

        // TODO: CarbonFootprint Route
        // GET /api/analysis/householdReport/carbonFootprint/{accountId}
        [HttpGet("householdReport/carbonFootprint/{accountId}/{year}-{month}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetCarbonFootprintData(Guid accountId,int month, int year)
        {
            List<CarbonFootprint> result = new List<CarbonFootprint>();

            try
            {
                result.Add(await _carbonFootprintService.GetCarbonFootprintAsync(accountId, month, year));
            }
            catch(AccountNotFoundException ex)
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
    }
}
