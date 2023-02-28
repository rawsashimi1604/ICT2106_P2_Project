using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.API.Controllers.NotificationAPIs.ViewModels;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;
using SmartHomeManager.Domain.NotificationDomain.Services;


namespace SmartHomeManager.API.Controllers.NotificationAPIs
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : Controller
    {

        private readonly SendNotificationService _sendNotificationService;
        private readonly ReceiveNotificationService _receiveNotificationService;

        // Dependency Injection of repos to services...
        public NotificationController(INotificationRepository notificationRepository, IGenericRepository<Account> mockAccountRepository)
        {
            _sendNotificationService = new(notificationRepository, mockAccountRepository);
            _receiveNotificationService = new(notificationRepository, mockAccountRepository);
        }

        // API routes....

        // GET /api/notification/all
        [HttpGet("all")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllNotifications()
        {
            // Map notfications to DTO....
            List<GetNotificationObjectDTO> getNotifications = new List<GetNotificationObjectDTO>();

            IEnumerable<Notification> notifications;
            NotificationResult notificationResult;
            int statusCode;
            string statusMessage;


            (notificationResult, notifications) = (await _receiveNotificationService.GetAllNotificationsAsync());

            // Get the status code and coressponding message...
            (statusCode, statusMessage) = MapNotificationResult(notificationResult);

            // Account not found or DB Error....
            if (notificationResult == NotificationResult.Error_AccountNotFound ||
                notificationResult == NotificationResult.Error_DBReadFail ||
                notificationResult == NotificationResult.Error_Other)
            {
                return StatusCode(statusCode, CreateResponseDTO(getNotifications, statusCode, statusMessage));
            }


            foreach (var notification in notifications)
            {
                getNotifications.Add(new GetNotificationObjectDTO
                {
                    NotificationId = notification.NotificationId,
                    AccountId = notification.AccountId,
                    NotificationMessage = notification.NotificationMessage,
                    SentTime = notification.SentTime,
                });
            }

            // Success path...
            return StatusCode(statusCode, CreateResponseDTO(getNotifications, statusCode, statusMessage));
        }

        // TODO:    GET /api/notification/{accountId}
        [HttpGet("{accountId}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetNotificationById(Guid accountId)
        {

            // Map notfications to DTO....
            List<GetNotificationObjectDTO> getNotifications = new List<GetNotificationObjectDTO>();

            // Use the service here...
            IEnumerable<Notification> notifications;
            NotificationResult notificationResult;
            int statusCode;
            string statusMessage;

            (notificationResult, notifications) = await _receiveNotificationService.GetNotificationsAsync(accountId);

            // Get the status code and coressponding message...
            (statusCode, statusMessage) = MapNotificationResult(notificationResult);


            // Account not found or DB Error....
            if (notificationResult == NotificationResult.Error_AccountNotFound ||
                notificationResult == NotificationResult.Error_DBReadFail ||
                notificationResult == NotificationResult.Error_Other)
            {
                return StatusCode(statusCode, CreateResponseDTO(getNotifications, statusCode, statusMessage));
            }

            foreach (var notification in notifications)
            {
                getNotifications.Add(new GetNotificationObjectDTO
                {
                    NotificationId = notification.NotificationId,
                    AccountId = notification.AccountId,
                    NotificationMessage = notification.NotificationMessage,
                    SentTime = notification.SentTime,
                });
            }

            return StatusCode(statusCode, CreateResponseDTO(getNotifications, statusCode, statusMessage));
        }

        // TODO:    POST /api/notification
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddNotification([FromBody] AddNotificationDTO clientDTO)
        {

            // Map notfications to DTO....
            List<GetNotificationObjectDTO> getNotifications = new List<GetNotificationObjectDTO>();
            int statusCode;
            string statusMessage;
            NotificationResult notificationResult;

            (notificationResult, Notification? notification) = await _sendNotificationService
                .SendNotification(
                clientDTO.NotificationObject.Message,
                clientDTO.NotificationObject.AccountId
            );

            // Get the status code and coressponding message...
            (statusCode, statusMessage) = MapNotificationResult(notificationResult);

            // Account not found or DB Error....
            if (notificationResult == NotificationResult.Error_AccountNotFound ||
                notificationResult == NotificationResult.Error_DBInsertFail ||
                notificationResult == NotificationResult.Error_Other)
            {

                return StatusCode(statusCode, CreateResponseDTO(getNotifications, statusCode, statusMessage));

            }
            
            getNotifications.Add(new GetNotificationObjectDTO
            {
                NotificationId = notification.NotificationId,
                AccountId = notification.AccountId,
                NotificationMessage = notification.NotificationMessage,
                SentTime = notification.SentTime,
            });

            return StatusCode(statusCode, CreateResponseDTO(getNotifications, statusCode, statusMessage));
        }

        private GetNotificationDTO CreateResponseDTO(List<GetNotificationObjectDTO> notificationList, int statusCode, string statusMessage)
        {
            return new GetNotificationDTO
            {
                NotificationObjects = notificationList,
                ResponseObject = new ResponseObjectDTO
                {
                    StatusCode = statusCode,
                    ServerMessage = statusMessage
                }
            };
        }

        private Tuple<int, string> MapNotificationResult(NotificationResult notificationResult)
        {
            const string Success = "SUCC: ";
            const string ClientError = "CLIENT_ERR: ";
            const string ServerError = "SERVER_ERR: ";

            switch (notificationResult)
            {
                case NotificationResult.Success:
                    return Tuple.Create(200, Success + "Success!");
                case NotificationResult.Error_AccountNotFound:
                    return Tuple.Create(400, ClientError + "AccountId Not Found.");
                case NotificationResult.Error_DBInsertFail:
                    return Tuple.Create(500, ServerError + "DB Insert Fail.");
                case NotificationResult.Error_DBReadFail:
                    return Tuple.Create(500, ServerError + "DB Read Fail.");
                case NotificationResult.Error_Other:
                    return Tuple.Create(500, ServerError + "Internal Server Error.");
                default:
                    return Tuple.Create(500, ServerError + "Internal Server Error.");
            }
        }
    }
}
