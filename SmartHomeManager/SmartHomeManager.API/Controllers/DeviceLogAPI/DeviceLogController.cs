using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DTO;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Services;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.DeviceLoggingDomain.Mocks;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartHomeManager.API.Controllers.DeviceLogAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceLogController : ControllerBase
    {

        private readonly DeviceLogReadService _logReadService;
        private readonly DeviceLogWriteService _logWriteService;


        public DeviceLogController(IDeviceLogRepository deviceLogRepository)
        {
            _logReadService = new DeviceLogReadService(deviceLogRepository);
            _logWriteService = new DeviceLogWriteService(deviceLogRepository);

        }

        private int getDeviceWatts(Guid deviceId)
        {
            // get watt of device
            var watt = 0;
            return watt;
        }

        // GET: api/DeviceLog
        [HttpGet]
        public async Task<ActionResult<DeviceLog>> GetAllDeviceLogs()
        {
            return Ok(await _logReadService.GetAllDeviceLogs());
        }


        /*        // PUT: api/DeviceLogs/5
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPut("{id}")]
                public async Task<IActionResult<GetDeviceLogWebRequest> GetDeviceLog(DateTime date, DateTime startTime, DateTime endTime)
                {
                     var result = await _logReadService.GetDeviceLogByDateAndTime(date, startTime, endTime);
                     if (result == null) return NotFound();
                     return result; 
                }
        */

        // get log by their date, start time,end time, device id.
        // once found log bring out their device watt usage

        // GET: api/Analytics/deviceId?startTime=xxxx&&endTime=xxxx
        [HttpGet("daily/{deviceId}")]
        public ActionResult<IEnumerable<DeviceLog>> GetDailyDeviceLog(Guid deviceId, DateTime endTime)
        {
            var date = DateTime.Now.Date;
            var totalUsage = 0;
            var totalActivity = 0;
            var result = _logReadService.GetDeviceLogByDateAndTime(deviceId, date, endTime);
            if (!result.Any()) return NotFound();
            foreach (var item in result)
            {
                totalUsage += item.DeviceEnergyUsage;
                totalActivity += item.DeviceActivity;
            }
            double[] res = { totalUsage, totalActivity };
            return Ok(res);
        }


        [HttpGet("{profileId}")]
        public ActionResult<IEnumerable<Device>> GetAllDevicesInProfile(Guid profileId)
        {
            //var result = _logReadService.GetAllDevicesInProfile(profileId);
            string[] result = { "33333333-3333-3333-3333-333333333333", "44444444-4444-4444-4444-444444444444" };
            if (!result.Any()) return NotFound();
            return Ok(result);
        }
        // date passed shld be start date of the week
        // GET: api/Analytics/DeviceLog/deviceId?date=xxxxxx
        [HttpGet("{deviceId}/{date}")]
        [Consumes("application/json")]
        [Produces("application/json")]

        public ActionResult<IEnumerable<DeviceLog>> GetWeeklyDeviceLog(Guid deviceId, DateTime date)
        {
            var totalUsage = 0;
            var totalActivity = 0;
            var result = _logReadService.GetDeviceLogByDay(deviceId, date);
            // if (!result.Any()) return NotFound();
            foreach (var item in result)
            {
                totalUsage += item.DeviceEnergyUsage;
                totalActivity += item.DeviceActivity;
            }
            double[] res = { totalUsage, totalActivity };
            return Ok(res);
        }


        // this is update from switching off device
        // PUT: api/DeviceLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceLog(DateTime date, Guid deviceId, EditDeviceLogWebRequest deviceLogWebRequest)
        {
            var res = await _logReadService.GetDeviceLogByDate(date, deviceId, true);

            if (res == null) return BadRequest();

            var endTime = DateTime.Now;
            var startTime = deviceLogWebRequest.DateLogged.TimeOfDay.TotalSeconds;
            var deviceUsage = deviceLogWebRequest.DeviceActivity;
            var deviceActivity = deviceLogWebRequest.DeviceEnergyUsage;
            var deviceWatt = getDeviceWatts(deviceId);
            // calculating new usage and activity
            var timeDifference = (endTime.TimeOfDay.TotalSeconds - startTime) / 3600;
            var totalWatts = timeDifference * deviceWatt;


            await _logWriteService.UpdateDeviceLog(deviceId, timeDifference, totalWatts, endTime, false);

            return NoContent();

        }


        /*        // GET: api/Analytics/GetDevicesInProfile/profileId
                [HttpGet("GetDevicesInProfile/{profileId}")]
                public ActionResult<IEnumerable<Device>> GetDevicesFromProfile(Guid profileId)
                {
                    var result = _logReadService.GetAllDevicesInProfile(profileId);
                    if (!result.Any()) return NotFound();

                    return Ok(result);
                }*/


        // POST: api/DeviceLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetDeviceLogWebRequest>> PostDeviceLog(CreateDeviceLogWebRequest deviceLogWebRequest)
        {
            var resp = await _logWriteService.AddDeviceLog(deviceLogWebRequest.DeviceId);
            return Ok(resp);
        }


    }
}
