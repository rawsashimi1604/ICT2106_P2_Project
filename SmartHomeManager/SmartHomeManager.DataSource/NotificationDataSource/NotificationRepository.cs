using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartHomeManager.DataSource.NotificationDataSource
{
    public class NotificationRepository : INotificationRepository
    {


        private readonly ApplicationDbContext _applicationDbContext;

        public NotificationRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Notification>> GetAllByIdAsync(Guid id)
        {
            IEnumerable<Notification> query = await _applicationDbContext.Notifications.ToListAsync();
            IEnumerable<Notification> result = query.Where(x => x.AccountId == id);
            return result;
        }

        public async Task<bool> AddAsync(Notification entity)
        {
            
            try
            {
                // Attempt to add entity to db, check if operation was successful.
                await _applicationDbContext.Notifications.AddAsync(entity);
                //IEnumerable<Account> accounts =
                //    await _applicationDbContext.Accounts.ToListAsync();

                //foreach (Account account in accounts)
                //{
                //    System.Diagnostics.Debug.WriteLine(account.AccountId);
                //}

                // SaveChangesAsync() returns a integer of how many items was added to db.
                // If 0, means nothing was added
                // If >=1, means at least entity was added...
                bool success = await _applicationDbContext.SaveChangesAsync() > 0;
                return success;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> DeleteAsync(Notification entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            // TODO: actual implementation of database get all...
            return await _applicationDbContext.Notifications.ToListAsync();

            // For temp data uncomment this...    
            //return _tempData;
        }


        public async Task<Notification?> GetByIdAsync(Guid id)
        {

            //return await _applicationDbContext.Notifications.(id);
            //Notification? result = _applicationDbContext.Find(id);
            //yield return result;
            //throw new NotImplementedException();
            return await _applicationDbContext.Notifications.FindAsync(id);
        }


        public async Task<bool> SaveAsync() 
        {
            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> UpdateAsync(Notification entity)
        {
            throw new NotImplementedException();
        }

        

       
    }
}
