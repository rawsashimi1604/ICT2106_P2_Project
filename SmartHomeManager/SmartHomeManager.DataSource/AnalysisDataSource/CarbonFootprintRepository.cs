using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.AnalysisDataSource
{
    public class CarbonFootprintRepository : ICarbonFootprintRepository
    {

        private readonly ApplicationDbContext _applicationDbContext;

        public CarbonFootprintRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddAsync(CarbonFootprint entity)
        {
            try
            {
                // Attempt to add entity to db, check if operation was successful.
                await _applicationDbContext.CarbonFootprints.AddAsync(entity);
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

        public Task<bool> DeleteAsync(CarbonFootprint entity)
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

        public async Task<IEnumerable<CarbonFootprint>> GetAllAsync()
        {
            return await _applicationDbContext.CarbonFootprints.ToListAsync();
        }

        public async Task<CarbonFootprint?> GetByIdAsync(Guid id)
        {
            return await _applicationDbContext.CarbonFootprints.FindAsync(id);
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

        public Task<bool> UpdateAsync(CarbonFootprint entity)
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

        public async Task<CarbonFootprint?> GetCarbonFootprintByMonthAndYear(int month, int year)
        {
            CarbonFootprint? data = await _applicationDbContext
                .CarbonFootprints
                .Where(cf => cf.YearOfAnalysis == year && cf.MonthOfAnalysis == month)
                .FirstOrDefaultAsync();

            return data;
        }
    }
}
