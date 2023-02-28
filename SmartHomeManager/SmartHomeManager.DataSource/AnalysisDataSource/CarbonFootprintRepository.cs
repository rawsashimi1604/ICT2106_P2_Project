using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.AnalysisDataSource
{
    public class CarbonFootprintRepository : IGenericRepository<CarbonFootprint>
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
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CarbonFootprint>> GetAllAsync()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
