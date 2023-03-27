using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.DeviceDomain.Services;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities.DTOs;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHomeManager.API.Controllers.DeviceAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterDeviceController : ControllerBase
    {
        private readonly RegisterDeviceService _registerDeviceService;

        public RegisterDeviceController(IDeviceRepository deviceRepository, IDeviceTypeRepository deviceTypeRepository)
        {
            _registerDeviceService = new(deviceRepository, deviceTypeRepository);
        }

        // GET api/<RegisterDeviceController>/GetAllDevices
        [HttpGet("GetAllDevice/{deviceName}")]
        public async Task<IEnumerable<string>> GetAllDevice(String deviceName)
        {
            IEnumerable<Device> devices = await _registerDeviceService.GetAllDevicesAsync();
            return devices.Where(device => device.DeviceTypeName == deviceName).Select(device => $"{device.DeviceName} - {device.DeviceId}");
        }

        // GET api/<RegisterDeviceController>/GetAllDeviceTypes
        [HttpGet("GetAllDeviceTypes")]
        public async Task<IEnumerable<string>> GetAllDeviceTypes()
        {
            IEnumerable<DeviceType> deviceTypes = await _registerDeviceService.GetAllDevicesTypeAsync();
            return deviceTypes.Select(deviceType => deviceType.DeviceTypeName);
        }

        // POST api/<RegisterDeviceController>/RegisterDevice
        [HttpPost("RegisterDevice")]
        public async Task<ActionResult> RegisterDevice([FromBody] DeviceWebRequest deviceWebRequest)
        {
            if (await _registerDeviceService.RegisterDeviceAsync(deviceWebRequest.DeviceName, deviceWebRequest.DeviceBrand, deviceWebRequest.DeviceModel, deviceWebRequest.DeviceTypeName, deviceWebRequest.DeviceWatts, deviceWebRequest.DeviceSerialNumber, deviceWebRequest.AccountId))
            {
                return Ok("RegisterDevice() success!");
            }

            return BadRequest("RegisterDevice() failed!");
        }

        // POST api/<RegisterDeviceController>/AddDeviceType
        [HttpPost("AddDeviceType")]
        public async Task<ActionResult> AddDeviceType([FromBody] DeviceType deviceType)
        {
            if (await _registerDeviceService.AddDeviceTypeAsync(deviceType))
            {
                return Ok("AddDeviceType() success!");
            }

            return BadRequest("AddDeviceType() failed!");
        }
    }
}
