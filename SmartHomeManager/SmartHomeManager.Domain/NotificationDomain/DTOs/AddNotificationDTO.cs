namespace SmartHomeManager.API.Controllers.NotificationAPIs.ViewModels
{
    public class AddNotificationDTO : IDataObjectDTO
    {
        public string Message { get; set; }
        public Guid AccountId { get; set; }
    }
}
