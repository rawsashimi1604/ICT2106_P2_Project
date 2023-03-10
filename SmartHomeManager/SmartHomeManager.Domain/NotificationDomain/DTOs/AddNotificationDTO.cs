using SmartHomeManager.Domain.Common.DTOs;

namespace SmartHomeManager.API.Controllers.NotificationAPIs.DTOs
{
    public class AddNotificationDTO : RequestDTO
    {
        public AddNotificationDTOData Request { get; set; }
    }

    public class AddNotificationDTOData : IDTOData
    {
        public string Message { get; set; }
        public Guid AccountId { get; set; }
    }
}
