using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces
{
    public interface IDeviceLogsFactory
    {
        private Guid LogId { get; set; }

        private DateTime? EndTime { get; set; }

        private DateTime DateLogged { get; set; }

        private double DeviceEnergyUsage { get; set; }

        private double DeviceActivity { get; set; }

        private bool DeviceState { get; set; }

        private Guid DeviceId { get; set; }

        private Guid RoomId { get; set; }

        private Device Device { get; set; }

        private Room Room { get; set; }
        
        private createDailyLog()
        {
            return new DailyLogs();
        }

        private createWeeklyLog()
        {
            return new WeeklyLogs();
        }
    }
}
