using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.DeviceEnergyLimit;
using SmartHomeManager.Domain.DeviceEnergyLimit.Entities.DTOs;
using SmartHomeManager.Domain.DeviceEnergyLimit.Interfaces;
using SmartHomeManager.Domain.RoomDomain.DTOs.Responses;
using SmartHomeManager.Domain.RoomDomain.Services;

namespace SmartHomeManager.API.Controllers.DeviceEnergyLimitAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceEnergyLimitController : ControllerBase
    {
        private readonly DeviceEnergyLimitService _deviceEnergyLimitService;

        public DeviceEnergyLimitController(IDeviceEnergyLimitRepository deviceEnergyLimitRepository)
        {
            _deviceEnergyLimitService = new DeviceEnergyLimitService(deviceEnergyLimitRepository);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnergyLimit>> GetDeviceEnergyLimitById(Guid id)
        {
            var energyLimit = await _deviceEnergyLimitService.getDeviceEnergyLimitById(id);
            if (energyLimit == null)
            {
                return NotFound();
            }
            return Ok(energyLimit);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnergyLimit>>> GetAllDeviceEnergyLimit()
        {
            var energyLimits = await _deviceEnergyLimitService.getAllDeviceEnergyLimit();
            return Ok(energyLimits);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeviceEnergyLimit(Guid id, EnergyLimit energyLimit)
        {
            var updatedEnergyLimit = await _deviceEnergyLimitService.updateDeviceEnergyLimit(id, energyLimit);

            if (updatedEnergyLimit == null)
            {
                return NotFound();
            }

            if (updatedEnergyLimit.DeviceEnergyUsage > updatedEnergyLimit.DeviceEnergyLimits)
            {
                await _deviceEnergyLimitService.SendNotification(updatedEnergyLimit);
            }

            return NoContent();
        }
    }
}
