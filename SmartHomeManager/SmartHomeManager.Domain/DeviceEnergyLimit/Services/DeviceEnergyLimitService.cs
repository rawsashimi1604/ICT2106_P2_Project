using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceEnergyLimit.Entities;
using SmartHomeManager.Domain.DeviceEnergyLimit.Entities.DTOs;
using SmartHomeManager.Domain.DeviceEnergyLimit.Interfaces;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.EnergyProfileDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.DTOs.Responses;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SmartHomeManager.Domain.DeviceEnergyLimit
{
    public class DeviceEnergyLimitService : IDeviceEnergyThreshold
    {

        private readonly IDeviceEnergyLimitRepository _deviceEnergyLimitRepository;

        public DeviceEnergyLimitService(IDeviceEnergyLimitRepository deviceEnergyLimitRepository)
        {
            _deviceEnergyLimitRepository = deviceEnergyLimitRepository;
        }




        public async Task<EnergyLimit?> getDeviceEnergyLimitById(Guid id)
        {
            var res = await _deviceEnergyLimitRepository.Get(id);
            if (res == null) return null;

            var ret = new EnergyLimit
            {
                DeviceName = res.DeviceName,
                DeviceTypeName = res.DeviceTypeName,
                RoomId = res.RoomId,
                DeviceBrand = res.DeviceBrand,
                AccountId = res.AccountId
            };

            return ret;
        }

        //public async Task<IEnumerable<EnergyLimit>> getAllDeviceEnergyLimit()
        //{
        //    IEnumerable<EnergyLimit> deviceEnergyLimit = await _deviceEnergyLimitRepository.GetAllAsync();

        //    if (deviceEnergyLimit == null)
        //    {
        //        return Enumerable.Empty<EnergyLimit>();
        //    }

        //    return deviceEnergyLimit;
        //}

        //public async Task<IEnumerable<SmartHomeManager.Domain.DeviceEnergyLimit.Entities.DeviceEnergyLimit>> setDeviceEnergyLimit(Guid model) 
        //{
        //    var res = await _deviceEnergyLimitRepository.Get(model);
        //    if (res == null) return;


        //    _deviceEnergyLimitRepository.Update(res);
        //    await _deviceEnergyLimitRepository.SaveChangesAsync();
        //}

        public async Task<IEnumerable<SmartHomeManager.Domain.DeviceEnergyLimit.Entities.DeviceEnergyLimit>> getAllDeviceEnergyLimit()
        {
            var result = await _deviceEnergyLimitRepository.GetAll();
            var resp = result.Select(device => new EnergyLimit
            {
                DeviceName = device.DeviceName,
                DeviceSerialNumber = device.DeviceSerialNumber,
                AccountId = device.DeviceId,
                DeviceTypeName = device.DeviceTypeName,
                DeviceEnergyLimits = device.DeviceEnergyLimits,
                DeviceModel = device.DeviceModel,
                DeviceBrand = device.DeviceBrand,
                RoomId = device.RoomId,
                ProfileId = device.ProfileId,

            }).ToList();

            return (IEnumerable<Entities.DeviceEnergyLimit>)resp;
        }

    }
}