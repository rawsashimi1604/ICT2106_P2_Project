﻿namespace SmartHomeManager.API.Controllers.NotificationAPIs.ViewModels
{
    public class GetNotificationViewModel
    {
        public Guid NotificationId { get; set; }
        public Guid AccountId { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime SentTime { get; set; }
    }
}