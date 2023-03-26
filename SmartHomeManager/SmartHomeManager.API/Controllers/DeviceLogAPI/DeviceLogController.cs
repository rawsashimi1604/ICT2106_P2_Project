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
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.DeviceLoggingDomain.Mocks;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SmartHomeManager.Domain.RoomDomain.Services;
using SmartHomeManager.Domain.RoomDomain.Mocks;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Services;

namespace SmartHomeManager.API.Controllers.DeviceLogAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceLogController : ControllerBase
    {

        private readonly DeviceLogReadService _logReadService;
        private readonly DeviceLogWriteService _logWriteService;
        private readonly RoomReadService _roomReadService;


        public DeviceLogController(IDeviceLogRepository deviceLogRepository, IRoomRepository roomRepository, IDeviceInformationServiceMock deviceInformationService)
        {
           _logReadService = new DeviceLogReadService(deviceLogRepository);
            _logWriteService = new DeviceLogWriteService(deviceLogRepository);
            _roomReadService = new RoomReadService(roomRepository, deviceInformationService);
            
        }
 

        private int getDeviceWatts(Guid deviceId) {
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

        [HttpGet("getRooms")]
        public ActionResult<IList<Room>> GetAllRooms() {
            var result = _roomReadService.GetRoomsByAccountId(Guid.Parse("11111111-1111-1111-1111-111111111111"));
           
            return Ok(result);
        }


        // get log by their date, start time,end time, device id.
        // once found log bring out their device watt usage

        // GET: api/Analytics/deviceId?startTime=xxxx&&endTime=xxxx
        [HttpGet("daily/{deviceId}")]
        public ActionResult<object> GetDailyDeviceLog(Guid deviceId)
        {
            var date = DateTime.Parse("2023-02-19 10:00:00").Date;
            var totalUsage = 0.0;
            var totalActivity = 0.0;
            var deviceStateStr = "";
            var result = _logReadService.GetDeviceLogByDay(deviceId, date);
            if (!result.Any()) return NotFound();
            foreach (var item in result)
            {
                totalUsage += item.DeviceEnergyUsage;
                totalActivity += item.DeviceActivity;
            }
            var deviceState = result.Last().DeviceState;
            if (deviceState == false)
            {
                deviceStateStr = "Off";
            }
            else {
                deviceStateStr = "Running";
            }
            var resultObject = new { DeviceId = deviceId, DeviceState = deviceStateStr, TotalUsage = totalUsage, TotalActivity = totalActivity };
            return Ok(resultObject);
        }


        [HttpGet("getDevices/{roomId}")]
        public ActionResult<IEnumerable<Device>> GetAllDevicesInRoom(Guid roomId)
        {
            var result = _roomReadService.GetDevicesInRoom(roomId);
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
            var totalUsage = 0.0;
            var totalActivity = 0.0;
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
        [HttpPut("stateOff/{deviceId}")]
        [Consumes("application/json")]
        [Produces("application/json")]

        public async Task<ActionResult<object>> PutDeviceLog(Guid deviceId)
        {
            // Get the latest entry from the database
            var latestLogEntry = await _logReadService.GetLatestDeviceLog(deviceId);

            // Check if a log entry was found
            if (latestLogEntry == null)
            {
                return NotFound();
            }

            var date = latestLogEntry.DateLogged;

            // Retrieve all device logs for the given date and device ID
            var res = await _logReadService.GetDeviceLogByDate(date, deviceId, true);

            foreach (var item in res)
            {
                var endTime = DateTime.Now;
                var startTime = item.DateLogged.TimeOfDay.TotalSeconds;
                var deviceWatt = getDeviceWatts(deviceId);
                // calculating new usage and activity
                var timeDifference = (endTime.TimeOfDay.TotalSeconds - startTime) / 3600;
                var totalWatts = deviceWatt * timeDifference;

                await _logWriteService.UpdateDeviceLog(date, item.DeviceId, timeDifference, totalWatts, endTime, false);
            }

            var dailyLogResult = GetDailyDeviceLog(deviceId);
            return dailyLogResult;
        }


        /*[HttpPut("stateOn/{deviceId}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> PutDeviceLog(DateTime date, Guid deviceId)
        {
            var res = await _logReadService.GetDeviceLogByDate(date, deviceId, true);

            if (res == null)
            {

                await _logWriteService.AddDeviceLog(deviceId);
            }
            else
            {
                var endTime = DateTime.Now;
                var startTime = res.DateLogged.TimeOfDay.TotalSeconds;
                var deviceWatt = getDeviceWatts(deviceId);
                // calculating new usage and activity
                var timeDifference = (endTime.TimeOfDay.TotalSeconds - startTime) / 3600;
                var totalWatts = timeDifference * deviceWatt;


                await _logWriteService.UpdateDeviceLog(deviceId, timeDifference, totalWatts, endTime, false);

            }

            return NoContent();

        }*/



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
        public async Task<ActionResult<GetDeviceLogWebRequest>> PostDeviceLog(CreateDeviceLogWebRequest deviceLogWebRequest )
        {
        var resp = await _logWriteService.AddDeviceLog(deviceLogWebRequest.DeviceId);
        return Ok(resp);
        }


    }
}
