using System.Text.RegularExpressions;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.Common.Exceptions;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;

namespace SmartHomeManager.Domain.NotificationDomain.Services
{
    public class SendNotificationProxy : ISendNotification
    {
        private readonly ISendNotification _sendNotificationService;
        private readonly AccountService _accountService;

        public SendNotificationProxy(ISendNotification sendNotificationService, IAccountRepository accountRepository)
        {
            this._sendNotificationService = sendNotificationService;
            _accountService = new AccountService(accountRepository);
        }

        public async Task<Notification?> SendNotification(string notificationMessage, Guid accountId)
        {
            //Include only validation and exception

            var account = await _accountService.GetAccountByAccountId(accountId);

            //Check if account exist
            if (account == null)
            {
                throw new AccountNotFoundException();
            }

            return await _sendNotificationService.SendNotification(notificationMessage,accountId);

        }
    }
}


