﻿using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces
{
    public interface IDeviceLogRepository
    {
        // bfe log repo have to state the methods needed from ds
        IEnumerable<DeviceLog> Get(DateTime date, DateTime startTime, DateTime endTime);

        Task<IEnumerable<DeviceLog>> GetAll();

        //still missing write functions



    }
}