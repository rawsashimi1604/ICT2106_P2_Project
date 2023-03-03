using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AnalysisDomain.Services
{
    internal class ForecastService { 

        private readonly IForecastRepository _forecastRepository;
        private readonly MockAccountService _mockAccountService;

        public ForecastService(IForecastRepository forecastRepository, IGenericRepository<Account> mockAccountRepository)
        {
            _forecastRepository = forecastRepository;
            _mockAccountService = new MockAccountService(mockAccountRepository);
        }

        public async Task<Tuple<IEnumerable<ForecastChart>?>> GetHouseHoldForecast(Guid accountid)
        {
            var accountToBeFound = await _mockAccountService.GetAccount(accountid);
            IEnumerable<ForecastChart> allForecastChart = null;

            //Check if account exist
            if (accountToBeFound == null)
            {
                System.Diagnostics.Debug.WriteLine("Account not found");
                return Tuple.Create(allForecastChart);
            }
            allForecastChart = (IEnumerable<ForecastChart>?)await _forecastRepository.GetAllByIdAsync(accountid);

            return Tuple.Create(allForecastChart);
        }
    }
}
