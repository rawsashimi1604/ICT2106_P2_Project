﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.Common.DTOs
{
    public enum ResponseDTOType
    {
        NOTIFICATION_GETALL,
        NOTIFICATION_GETBYACCOUNT,
        NOTIFICATION_ADD,
        DEVICE_GETBYACCOUNT,
        ANALYSIS_CARBONFOOTPRINT_GETBYACCOUNTMONTHYEAR,
        ANAYLSIS_FORECAST_GETBYACCOUNTTIMESPAN
    }
}