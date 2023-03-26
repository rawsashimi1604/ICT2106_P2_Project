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
        //private readonly EmailService _emailService;

        public DeviceEnergyLimitController(IDeviceEnergyLimitRepository deviceEnergyLimitRepository)
        {
            _deviceEnergyLimitService = new DeviceEnergyLimitService(deviceEnergyLimitRepository);

            //_emailService = emailService;
        }



        [HttpGet("{id}")]
        public async Task<bool> GetDeviceEnergyLimitById(Guid id)
        {
            var energyLimit = await _deviceEnergyLimitService.getDeviceEnergyLimitById(id);
            return energyLimit == null;
        }




        [RequireHttps]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnergyLimit>>> getAllDeviceEnergyLimit()
        {
            return Ok(await _deviceEnergyLimitService.getAllDeviceEnergyLimit());
        }


        [HttpPut]
        public async Task<IActionResult> PutDeviceEnergyLimit(Guid id, EnergyLimit energyLimit)
        {
            /* if (id != account.AccountId)
             {
                 return BadRequest();
             }

             _context.Entry(account).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!AccountExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }*/

            return NoContent();
        }

    }
}
