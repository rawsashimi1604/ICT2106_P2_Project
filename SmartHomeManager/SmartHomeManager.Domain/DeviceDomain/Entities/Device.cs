using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeManager.Domain.Common;

namespace SmartHomeManager.Domain.DeviceDomain.Entities
{
    [Index(nameof(DeviceSerialNumber), IsUnique = true)]
    public class Device : IEntity
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

        public string? DevicePassword { get; set; }

        [Required]
        public int DeviceWatts { get; set; }

        public Guid? RoomId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [ForeignKey("DeviceTypeName")]
        public DeviceType DeviceType { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        /*public ICollection<DeviceProfile> DeviceProfiles { get; set; }*/

        public DeviceCoordinate DeviceCoordinate { get; set; }
    }
}
