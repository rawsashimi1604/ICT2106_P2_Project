using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceEnergyLimit.Entities.DTOs
{
    public class EnergyLimit
    {

        [Required]
        public string DeviceName { get; set; }

        [Required]
        public string DeviceBrand { get; set; }

        [Required]
        public string DeviceModel { get; set; }

        [Required]
        public string DeviceTypeName { get; set; }

        [Required]
        public string DeviceSerialNumber { get; set; }

        [Required]
        public int DeviceEnergyLimits { get; set; }


        public Guid? RoomId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public Guid ProfileId { get; set; }
    }
}