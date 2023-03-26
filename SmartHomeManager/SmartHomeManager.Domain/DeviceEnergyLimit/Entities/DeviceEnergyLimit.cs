using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.DeviceEnergyLimit.Entities
{
    [Index(nameof(DeviceSerialNumber), IsUnique = true)]
    public class DeviceEnergyLimit
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DeviceId { get; set; }

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

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }


        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
    }
}