using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.Common.Exceptions;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartHomeManager.Domain.NotificationDomain.Services
{
    public class SendNotificationService : ISendNotification
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly MockAccountService _mockAccountService;

        public SendNotificationService(INotificationRepository notificationRepository, IGenericRepository<Account> mockAccountRepository)
        {
            _notificationRepository = notificationRepository;
            _mockAccountService = new MockAccountService(mockAccountRepository);
        }

        public async Task<Notification?> SendNotification(string notificationMessage, Guid accountId)
        {
            var account = await _mockAccountService.GetAccount(accountId);
            
            //Check if account exist
            if (account == null)
            {
                throw new AccountNotFoundException();
            }

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
