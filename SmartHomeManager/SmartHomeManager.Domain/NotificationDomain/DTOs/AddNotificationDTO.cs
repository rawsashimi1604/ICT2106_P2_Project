using SmartHomeManager.Domain.Common.DTOs;

namespace SmartHomeManager.API.Controllers.NotificationAPIs.DTOs
{
    public class AddNotificationDTO : IDataObjectDTO
    {
        public string Message { get; set; }
        public Guid AccountId { get; set; }
    }
}
