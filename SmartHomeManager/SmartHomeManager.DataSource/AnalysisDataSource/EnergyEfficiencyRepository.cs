using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.AnalysisDataSource
{
    public class EnergyEfficiencyRepository : IGenericRepository<EnergyEfficiency>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EnergyEfficiencyRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddAsync(EnergyEfficiency entity)
        {
            try
            {
                // Attempt to add entity to db, check if operation was successful.
                await _applicationDbContext.EnergyEfficiency.AddAsync(entity);
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

        public Task<bool> DeleteAsync(EnergyEfficiency entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EnergyEfficiency>> GetAllAsync()
        {
            // TODO: actual implementation of database get all...
            return await _applicationDbContext.EnergyEfficiency.ToListAsync();
            //throw new NotImplementedException();
            // For temp data uncomment this...    
            //return _tempData;
        }
        public async Task<EnergyEfficiency?> GetByIdAsync(Guid id)
        {
            return await _applicationDbContext.EnergyEfficiency.FindAsync(id);
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

        public async Task<bool> UpdateAsync(EnergyEfficiency entity)
        {
            try
            {
                //await _applicationDbContext.Update(entity);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
