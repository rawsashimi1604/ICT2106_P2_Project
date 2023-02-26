using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;

namespace SmartHomeManager.Domain.NotificationDomain.Services
{ 
    public class ReceiveNotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly MockAccountService _mockAccountService;


        public ReceiveNotificationService(INotificationRepository notificationRepository, IGenericRepository<Account> mockAccountRepository)
        {
            _notificationRepository = notificationRepository;
            _mockAccountService = new MockAccountService(mockAccountRepository);
        }

        public async Task<Tuple<NotificationResult, IEnumerable<Notification>>> GetAllNotificationsAsync()
        {
            // TODO: Pass in accountId
            IEnumerable<Notification> allNotification = null;
            try
            {
                allNotification = await _notificationRepository.GetAllAsync();
                return Tuple.Create(NotificationResult.Success, allNotification);
            }
            catch (Exception ex)
            {
                return Tuple.Create(NotificationResult.Error_DBReadFail, allNotification);
            }
        }

        // List, ArrayList, Array...
        public async Task<Tuple<NotificationResult, IEnumerable<Notification>?>> GetNotificationsAsync(Guid accountId)
        {

            // TODO: Create logic for Get Notifications by AccountId
            // Use GetAllByIdAsync
            // Check if account exists
            // Receive the top 5 most recent notifications by AccountId (filter the top 5 most recent)

            var accountToBeFound = await _mockAccountService.GetAccount(accountId);
            IEnumerable<Notification> allNotification = null;

            //Check if account exist
            if (accountToBeFound == null)
            {
                System.Diagnostics.Debug.WriteLine("Account not found");
                return Tuple.Create(NotificationResult.Error_AccountNotFound, allNotification);
            }
            allNotification = await _notificationRepository.GetAllByIdAsync(accountId);
            //Check if DBReadFail
            try
            {
                //Sort and get the latest 5 notifications
                IEnumerable<Notification> latest5Notification = allNotification.OrderBy(noti => noti.SentTime).TakeLast(5);
                return Tuple.Create(NotificationResult.Success, latest5Notification);
            }
            catch (Exception ex)
            {
                return Tuple.Create(NotificationResult.Error_DBReadFail, allNotification);
            }
        }
    }
}
