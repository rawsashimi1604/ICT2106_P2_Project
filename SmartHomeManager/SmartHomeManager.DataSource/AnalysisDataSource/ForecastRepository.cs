using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.AnalysisDataSource
{
    public class ForecastRepository : IForecastRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ForecastRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddAsync(ForecastChart entity)
        {
            try
            {
                // Attempt to add entity to db, check if operation was successful.
                await _applicationDbContext.ForecastCharts.AddAsync(entity);
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

        public Task<bool> DeleteAsync(ForecastChart entity)
        {
            try
            {
                _applicationDbContext.Remove(entity);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                _applicationDbContext.RemoveRange(id);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public async Task<IEnumerable<ForecastChart>> GetAllAsync()
        {
            return await _applicationDbContext.ForecastCharts.ToListAsync();
        }


        async Task<IEnumerable<Notification>> IForecastRepository.GetAllByIdAsync(Guid id)
        {
            IEnumerable<ForecastChart> query = await _applicationDbContext.ForecastCharts.ToListAsync();
            IEnumerable<ForecastChart> result = query.Where(x => x.AccountId == id);
            return (IEnumerable<Notification>)result;
        }

        public async Task<ForecastChart?> GetByIdAsync(Guid id)
        {
            return await _applicationDbContext.ForecastCharts.FindAsync(id);
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

        public Task<bool> UpdateAsync(ForecastChart entity)
        {
            try
            {
                _applicationDbContext.Update(entity);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        
    }
}


