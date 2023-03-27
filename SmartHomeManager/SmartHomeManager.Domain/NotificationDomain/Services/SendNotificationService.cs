using System.Text.RegularExpressions;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.Common.Exceptions;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;

namespace SmartHomeManager.Domain.NotificationDomain.Services
{
    public class SendNotificationService : ISendNotification
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly AccountService _accountService;

        public SendNotificationService(INotificationRepository notificationRepository, IAccountRepository accountRepository)
        {
            _notificationRepository = notificationRepository;
            _accountService = new AccountService(accountRepository);
        }

        public async Task<Notification?> SendNotification(string notificationMessage, Guid accountId)
        {
            var account = await _accountService.GetAccountByAccountId(accountId);

            //Remove symbol to prevent SQL injection
            notificationMessage = Regex.Replace(notificationMessage, "[^0-9A-Za-z _-]", "");

            // Generate notification object..
            Notification notificationToBeAdded = new Notification
            {
                AccountId = accountId,
                NotificationMessage = notificationMessage,
                SentTime = DateTime.Now,
                Account = account
            };

            bool result = await _notificationRepository.AddAsync(notificationToBeAdded);

            // If something went wrong...
            if (!result)
            {
                throw new DBInsertFailException();
            }

            return notificationToBeAdded;
        }

    }
}
