using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.AnalysisDataSource
{
    public class ForecastDataRepository : IForecastDataRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ForecastDataRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddAsync(ForecastChartData entity)
        {
            try
            {
                // Attempt to add entity to db, check if operation was successful.
                await _applicationDbContext.ForecastChartsData.AddAsync(entity);
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

        public Task<bool> DeleteAsync(ForecastChartData entity)
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

        public async Task<IEnumerable<ForecastChartData>> GetAllAsync()
        {
            return await _applicationDbContext.ForecastChartsData.ToListAsync();
        }


        public async Task<IEnumerable<ForecastChartData>> GetAllByIdAsync(Guid id)
        {
            IEnumerable<ForecastChartData> query = await _applicationDbContext.ForecastChartsData.ToListAsync();
            IEnumerable<ForecastChartData> result = query.Where(x => x.ForecastChartId == id);
            return result;
        }

        public async Task<ForecastChartData?> GetByIdAsync(Guid id)
        {
            return await _applicationDbContext.ForecastChartsData.FindAsync(id);
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

        public Task<bool> UpdateAsync(ForecastChartData entity)
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
