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

        private readonly SendNotificationService _sendNotificationService;
        private readonly ReceiveNotificationService _receiveNotificationService;

        // Dependency Injection of repos to services...
        public NotificationController(INotificationRepository notificationRepository, IAccountRepository accountRepository)
        {
            _sendNotificationService = new(notificationRepository, accountRepository);
            _receiveNotificationService = new(notificationRepository, accountRepository);
        }

        // API routes....

        // GET /api/notification/all
        [HttpGet("all")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllNotifications()
        {
            // Map notfications to DTO....
            List<GetNotificationDTO> getNotifications = new List<GetNotificationDTO>();

            IEnumerable<Notification> notifications = null;

            try
            {
                notifications = (await _receiveNotificationService.GetAllNotificationsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, DTONotificationFactory.getNotificationDTO(notifications, 500, ex.Message));
            }

            return StatusCode(200, DTONotificationFactory.getNotificationDTO(notifications, 200, "Success"));
            //foreach (var notification in notifications)
            //{
            //    getNotifications.Add(new GetNotificationObjectDTO
            //    {
            //        NotificationId = notification.NotificationId,
            //        AccountId = notification.AccountId,
            //        NotificationMessage = notification.NotificationMessage,
            //        SentTime = notification.SentTime,
            //    });
            //}

            //// Success path...
            //return StatusCode(
            //    200, 
            //    CreateResponseDTO(getNotifications, 200, "Success")
            //);
        }

        // TODO:    GET /api/notification/{accountId}
        [HttpGet("{accountId}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetNotificationById(Guid accountId)
        {

            // Map notfications to DTO....
            List<GetNotificationDTO> getNotifications = new List<GetNotificationDTO>();

            // Use the service here...
            IEnumerable<Notification> notifications = null;

            try
            {
                notifications = await _receiveNotificationService.GetNotificationsAsync(accountId);
            } 
            catch (AccountNotFoundException ex)
            {
                return StatusCode(500, DTONotificationFactory.getNotificationDTO(notifications, 400, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, DTONotificationFactory.getNotificationDTO(notifications, 500, ex.Message));
                
            }

            return StatusCode(200, DTONotificationFactory.getNotificationDTO(notifications, 200, "Success"));
            //foreach (var notification in notifications)
            //{
            //    getNotifications.Add(new GetNotificationObjectDTO
            //    {
            //        NotificationId = notification.NotificationId,
            //        AccountId = notification.AccountId,
            //        NotificationMessage = notification.NotificationMessage,
            //        SentTime = notification.SentTime,
            //    });
            //}

            //return StatusCode(200, CreateResponseDTO(getNotifications, 200, "Success"));
        }

        // TODO:    POST /api/notification
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddNotification([FromBody] AddNotificationDTO clientDTO)
        {

            // Map notfications to DTO....
            List<GetNotificationDTO> getNotifications = new List<GetNotificationDTO>();
            Notification? notification;

            try
            {
                notification = await _sendNotificationService
                .SendNotification(
                clientDTO.Message,
                clientDTO.AccountId
                );
            } 
            catch (AccountNotFoundException ex)
            {
                return StatusCode(400, DTONotificationFactory.getNotificationDTO(notifications, 400, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, DTONotificationFactory.getNotificationDTO(notifications, 500, ex.Message))
                );
            }

            getNotifications.Add(new GetNotificationDTO
            {
                NotificationId = notification.NotificationId,
                AccountId = notification.AccountId,
                NotificationMessage = notification.NotificationMessage,
                SentTime = notification.SentTime,
            });

            return StatusCode(200, CreateResponseDTO(getNotifications, 200, "Success"));
        }

        /*private GetNotificationDTO CreateResponseDTO(List<GetNotificationDTO> notificationList, int statusCode, string statusMessage)
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
        }*/
    }
}
