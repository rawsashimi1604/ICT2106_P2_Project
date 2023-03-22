using System.Runtime.InteropServices.ComTypes;
using SmartHomeManager.Domain.Common.DTOs;

namespace SmartHomeManager.API.Controllers.NotificationAPIs.DTOs
{

    public class GetNotificationDTO : ResponseDTO
    {
        
        public ResponseInformation Response { get; set; }
        public IEnumerable<GetNotificationDTOData> Data { get; set; }
    }

    public class GetNotificationDTOData : IDTOData
    {
        public Guid NotificationId { get; set; }
        public Guid AccountId { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime SentTime { get; set; }
    }
}
