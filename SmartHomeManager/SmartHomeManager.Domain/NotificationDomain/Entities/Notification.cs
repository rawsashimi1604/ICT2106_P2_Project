using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.NotificationDomain.Entities
{   
    public class Notification : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid NotificationId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public string NotificationMessage { get; set; }

        [Required]
        public DateTime SentTime { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
