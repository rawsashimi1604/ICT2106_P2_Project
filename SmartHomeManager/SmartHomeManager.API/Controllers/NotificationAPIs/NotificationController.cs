using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.API.Controllers.NotificationAPIs.ViewModels;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.Common.Exceptions;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;
using SmartHomeManager.Domain.NotificationDomain.Services;


namespace SmartHomeManager.API.Controllers.NotificationAPIs
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : Controller
    {

        private readonly ISendNotification _sendNotificationService;
        private readonly IReceiveNotification _receiveNotificationService;

        // Dependency Injection of repos to services...
        public NotificationController(IReceiveNotification receiveNotification,ISendNotification sendNotification)
        {
            this._sendNotificationService = sendNotification;
            _receiveNotificationService = receiveNotification;
        }

        // API routes....

        // GET /api/notification/all
        [HttpGet("all")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllNotifications()
        {
            // Map notfications to DTO....
            List<GetNotificationObjectDTO> getNotifications = new List<GetNotificationObjectDTO>();

            IEnumerable<NotificationDomain> notifications;

            try
            {
                notifications = (await _receiveNotificationService.GetAllNotificationsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, CreateResponseDTO(getNotifications, 500, ex.Message)
                );
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
            return StatusCode(
                200, 
                CreateResponseDTO(getNotifications, 200, "Success")
            );
        }

        // TODO:    GET /api/notification/{accountId}
        [HttpGet("{accountId}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetNotificationById(Guid accountId)
        {

            // Map notfications to DTO....
            List<GetNotificationObjectDTO> getNotifications = new List<GetNotificationObjectDTO>();

            // Use the service here...
            IEnumerable<NotificationDomain> notifications;

            try
            {
                notifications = await _receiveNotificationService.GetNotificationsAsync(accountId);
            } 
            catch (AccountNotFoundException ex)
            {
                return StatusCode(400, CreateResponseDTO(getNotifications, 400, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, CreateResponseDTO(getNotifications, 500, ex.Message)
                );
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

            return StatusCode(200, CreateResponseDTO(getNotifications, 200, "Success"));
        }

        // TODO:    POST /api/notification
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddNotification([FromBody] AddNotificationDTO clientDTO)
        {

            // Map notfications to DTO....
            List<GetNotificationObjectDTO> getNotifications = new List<GetNotificationObjectDTO>();
            NotificationDomain? notification;

            try
            {
                notification = await _sendNotificationService
                .SendNotification(
                clientDTO.NotificationObject.Message,
                clientDTO.NotificationObject.AccountId
                );
            } 
            catch (AccountNotFoundException ex)
            {
                return StatusCode(400, CreateResponseDTO(getNotifications, 400, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, CreateResponseDTO(getNotifications, 500, ex.Message)
                );
            }

            
            getNotifications.Add(new GetNotificationObjectDTO
            {
                NotificationId = notification.NotificationId,
                AccountId = notification.AccountId,
                NotificationMessage = notification.NotificationMessage,
                SentTime = notification.SentTime,
            });

            return StatusCode(200, CreateResponseDTO(getNotifications, 200, "Success"));
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
    }
}
