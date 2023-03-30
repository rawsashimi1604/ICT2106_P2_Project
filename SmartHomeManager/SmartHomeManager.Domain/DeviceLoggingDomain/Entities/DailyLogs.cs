using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartHomeManager.Domain.DeviceLoggingDomain.Entities
{
    public class DailyLogs
    {
        public void getLogs(DateTime date, Guid roomID);

    }

}
