using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.API.Controllers.NotificationAPIs.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.Common.DTOs;
using SmartHomeManager.Domain.Common.Exceptions;
using SmartHomeManager.Domain.NotificationDomain.DTOs;
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
        private readonly AbstractDTOFactory<GetNotificationDTO, AddNotificationDTO> _dtoFactory;

        // Dependency Injection of repos to services...
        public NotificationController(INotificationRepository notificationRepository, IAccountRepository accountRepository)
        {
            _sendNotificationService = new(notificationRepository, accountRepository);
            _receiveNotificationService = new(notificationRepository, accountRepository);
            _dtoFactory = new NotificationDTOFactory();
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
                return StatusCode(
                    500,
                    _dtoFactory.CreateResponseDTO(ResponseDTOType.GET_NOTIFICATION, notifications, 500, ex.Message)
                );
            }

            return StatusCode(
                200,
                _dtoFactory.CreateResponseDTO(
                    ResponseDTOType.GET_NOTIFICATION,
                    notifications,
                    200,
                    "Success!"
                )
            );
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
                return StatusCode(
                    400, 
                    _dtoFactory.CreateResponseDTO(ResponseDTOType.GET_NOTIFICATION, notifications, 400, ex.Message
                    )    
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    _dtoFactory.CreateResponseDTO(ResponseDTOType.GET_NOTIFICATION, notifications, 500, ex.Message
                    )
                );

            }

            return StatusCode(
                200,
                _dtoFactory.CreateResponseDTO(
                    ResponseDTOType.GET_NOTIFICATION,
                    notifications,
                    200,
                    "Success!"
                )
            );
        }

        // TODO:    POST /api/notification
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddNotification([FromBody] AddNotificationDTO clientDTO)
        {

            // Map notfications to DTO....
            List<GetNotificationDTO> getNotifications = new List<GetNotificationDTO>();
            List<Notification> notificationWrapper = new List<Notification>();
            Notification? notification;

            try
            {
                notification = await _sendNotificationService
                    .SendNotification(
                    clientDTO.Message,
                    clientDTO.AccountId
                );

                notificationWrapper.Add(notification);
            } 

            catch (AccountNotFoundException ex)
            {
                return StatusCode(
                    400,
                    _dtoFactory.CreateResponseDTO(ResponseDTOType.ADD_NOTIFICATION, notificationWrapper, 400, ex.Message
                    )
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    _dtoFactory.CreateResponseDTO(ResponseDTOType.ADD_NOTIFICATION, notificationWrapper, 500, ex.Message
                    )
                );
            }


            return StatusCode(
                200,
                _dtoFactory.CreateResponseDTO(
                    ResponseDTOType.ADD_NOTIFICATION,
                    notificationWrapper,
                    200,
                    "Success!"
                )
            );
        }
    }
}
