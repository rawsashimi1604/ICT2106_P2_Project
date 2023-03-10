using Org.BouncyCastle.Asn1.Ocsp;
using SmartHomeManager.API.Controllers.NotificationAPIs.DTOs;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.Common.DTOs;
using SmartHomeManager.Domain.NotificationDomain.Entities;

namespace SmartHomeManager.Domain.NotificationDomain.DTOs
{
    public class NotificationDTOFactory : AbstractDTOFactory<GetNotificationDTO, AddNotificationDTO>
    {
        public override RequestDTO<AddNotificationDTO> CreateRequestDTO (RequestDTOType type, IEnumerable<IEntity> data)
        {
            switch(type)
            {
                case RequestDTOType.ADD_NOTIFICATION:
                    return CreateAddNotificationDTO(
                        (IEnumerable<Notification>)data
                    );

                default:
                    return null;
            }
        }

        public override ResponseDTO<GetNotificationDTO> CreateResponseDTO (ResponseDTOType type, IEnumerable<IEntity> data, int statusCode, string statusMessage)
        {
            switch(type)
            {
                case ResponseDTOType.GET_NOTIFICATION:
                    return CreateGetNotificationDTO (
                        (IEnumerable<Notification>) data,
                        statusCode,
                        statusMessage
                    );

                case ResponseDTOType.ADD_NOTIFICATION:
                    return CreateGetNotificationDTO(
                        (IEnumerable<Notification>) data, 
                        statusCode, 
                        statusMessage
                    );

                default:
                    return null;
            }
        }

        // For GET /api/notifications/all
        // For GET /api/notifications/{accountId}
        public ResponseDTO<GetNotificationDTO> CreateGetNotificationDTO
        (
            IEnumerable<Notification> notifications,
            int statusCode,
            string statusMessage
        )
        {
            List<GetNotificationDTO> getNotifications = new List<GetNotificationDTO>();

            if (notifications != null)
            {
                foreach (var notification in notifications)
                {
                    getNotifications.Add(new GetNotificationDTO
                    {
                        NotificationId = notification.NotificationId,
                        AccountId = notification.AccountId,
                        NotificationMessage = notification.NotificationMessage,
                        SentTime = notification.SentTime,
                    });
                }
            }

            ResponseDTO<GetNotificationDTO> dto = new ResponseDTO<GetNotificationDTO>
            {
                Data = getNotifications,
                Response = new ResponseInformationDTO
                {
                    ServerMessage = statusMessage,
                    StatusCode = statusCode
                }
            };


            return dto;
        }

        // For POST /api/notifications/{accountId}
        public RequestDTO<AddNotificationDTO> CreateAddNotificationDTO
        (
            IEnumerable<Notification> notifications
        )
        {
            List<AddNotificationDTO> getNotifications = new List<AddNotificationDTO>();

            foreach (var notification in notifications)
            {
                getNotifications.Add(new AddNotificationDTO
                {
                    Message = notification.NotificationMessage,
                    AccountId = notification.AccountId
                });
            }

            return new RequestDTO<AddNotificationDTO>
            {
                Data = getNotifications
            };
        }
    }
}
