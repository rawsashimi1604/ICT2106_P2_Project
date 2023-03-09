using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
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

        public ReceiveNotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        
        }

        //public async Task<Tuple<NotificationResult, IEnumerable<Notification>>> GetAllNotificationsAsync()
        public async Task<IEnumerable<Entities.Notification>> GetAllNotificationsAsync()
        {
            // TODO: Pass in accountId
            IEnumerable<Entities.Notification> allNotification = null;
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
        public async Task<IEnumerable<Entities.Notification>?> GetNotificationsAsync(Guid accountId)
        {
            var accountToBeFound = await _accountService.CheckAccountExists(accountId);
            IEnumerable<Entities.Notification> allNotification = null;

            //Validation has been done in proxy

            allNotification = await _notificationRepository.GetAllByIdAsync(accountId);

            try
            {
                //Sort and get the latest 5 notifications
                IEnumerable<Entities.Notification> latest5Notification = allNotification.OrderBy(noti => noti.SentTime).TakeLast(5);
                return latest5Notification;
            }
            catch (Exception ex)
            {
                throw new DBReadFailException();
            }
        }
    }
}
