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

namespace SmartHomeManager.Domain.NotificationDomain.Proxies
{
    public class ReceiveNotificationProxy : IReceiveNotification
    {
        private readonly AccountService _accountService;
        private readonly IReceiveNotification _receiveNotificationService;

        public ReceiveNotificationProxy(IReceiveNotification receiveNotificationService, IAccountRepository accountRepository)
        {
            _receiveNotificationService = receiveNotificationService;
            _accountService = new AccountService(accountRepository);
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await _receiveNotificationService.GetAllNotificationsAsync();
        }


        // List, ArrayList, Array...
        public async Task<IEnumerable<Notification>?> GetNotificationsAsync(Guid accountId)
        {
            var accountToBeFound = await _accountService.CheckAccountExists(accountId);

            //Check if account exist
            if (accountToBeFound == false)
            {
                throw new AccountNotFoundException();
            }

            return await _receiveNotificationService.GetNotificationsAsync(accountId);
        }
    }
}
