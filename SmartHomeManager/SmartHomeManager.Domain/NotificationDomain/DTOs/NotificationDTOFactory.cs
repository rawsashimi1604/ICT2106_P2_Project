using Org.BouncyCastle.Asn1.Ocsp;
using SmartHomeManager.API.Controllers.NotificationAPIs.DTOs;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.Common.DTOs;
using SmartHomeManager.Domain.NotificationDomain.Entities;

namespace SmartHomeManager.Domain.NotificationDomain.DTOs
{
    public class NotificationDTOFactory : AbstractDTOFactory
    {

        public override ResponseDTO CreateResponseDTO(ResponseDTOType type, IEnumerable<IEntity> data, int statusCode, string statusMessage)
        {
            switch(type)
            {
                case ResponseDTOType.NOTIFICATION_GETALL:
                    return CreateGetNotificationDTO(
                        (IEnumerable<Notification>) data, 
                        statusCode, 
                        statusMessage
                    );

                case ResponseDTOType.NOTIFICATION_GETBYACCOUNT:
                    return CreateGetNotificationDTO(
                        (IEnumerable<Notification>) data,
                        statusCode,
                        statusMessage
                    );

                case ResponseDTOType.NOTIFICATION_ADD:
                    return CreateGetNotificationDTO(
                        (IEnumerable<Notification>)data,
                        statusCode,
                        statusMessage
                    );


                default:
                    return null;
            }
        }

        // For GET /api/notifications/all
        // For GET /api/notifications/{accountId}
        public GetNotificationDTO CreateGetNotificationDTO
        (
            IEnumerable<Notification> notifications,
            int statusCode,
            string statusMessage
        )
        {
            List<GetNotificationDTOData> getNotifications = new List<GetNotificationDTOData>();

            if (notifications != null)
            {
                foreach (var notification in notifications)
                {
                    getNotifications.Add(new GetNotificationDTOData
                    {
                        NotificationId = notification.NotificationId,
                        AccountId = notification.AccountId,
                        NotificationMessage = notification.NotificationMessage,
                        SentTime = notification.SentTime,
                    });
                }
            }

            return new GetNotificationDTO
            {
                Data = getNotifications,
                Response = new ResponseInformation
                {
                    ServerMessage = statusMessage,
                    StatusCode = statusCode
                }
            };
        }
    }
}
