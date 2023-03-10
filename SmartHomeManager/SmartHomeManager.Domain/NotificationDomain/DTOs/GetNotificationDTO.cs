using SmartHomeManager.Domain.Common.DTOs;

namespace SmartHomeManager.API.Controllers.NotificationAPIs.DTOs
{
    public class GetNotificationDTO : IDataObjectDTO
    {
        public Guid NotificationId { get; set; }
        public Guid AccountId { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime SentTime { get; set; }
    }
}
