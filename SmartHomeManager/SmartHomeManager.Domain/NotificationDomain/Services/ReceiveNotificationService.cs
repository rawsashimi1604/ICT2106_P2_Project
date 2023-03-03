using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.Common.Exceptions;
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

        //public async Task<Tuple<NotificationResult, IEnumerable<Notification>>> GetAllNotificationsAsync()
        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            // TODO: Pass in accountId
            IEnumerable<Notification> allNotification = null;
            try
            {
                allNotification = await _notificationRepository.GetAllAsync();
                return allNotification;
            }
            catch (Exception ex)
            {
                throw new DBReadFailException();
            }
        }

        // List, ArrayList, Array...
        public async Task<IEnumerable<Notification>?> GetNotificationsAsync(Guid accountId)
        {
            var accountToBeFound = await _mockAccountService.GetAccount(accountId);
            IEnumerable<Notification> allNotification = null;

            //Check if account exist
            if (accountToBeFound == null)
            {
                throw new AccountNotFoundException();
            }

            allNotification = await _notificationRepository.GetAllByIdAsync(accountId);

            try
            {
                //Sort and get the latest 5 notifications
                IEnumerable<Notification> latest5Notification = allNotification.OrderBy(noti => noti.SentTime).TakeLast(5);
                return latest5Notification;
            }
            catch (Exception ex)
            {
                throw new DBReadFailException();
            }
        }
    }
}
