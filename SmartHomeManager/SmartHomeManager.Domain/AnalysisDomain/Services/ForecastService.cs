using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
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
    public class ForecastService
    {

        private readonly IForecastRepository _forecastRepository;
        private readonly IForecastDataRepository _forecastDataRepository;
        private readonly AccountService _accountService;

        public ForecastService(IForecastRepository forecastRepository, IAccountRepository accountRepository)
        {
            _forecastRepository = forecastRepository;
            _accountService = new AccountService(accountRepository);
        }

        public async Task<IEnumerable<ForecastChart>?> GetHouseHoldForecast(Guid accountid)
        {
            var accountToBeFound = await _accountService.GetAccountByAccountId(accountid);
            IEnumerable<ForecastChart> allForecastChart = null;

            //Check if account exist
            if (accountToBeFound == null)
            {
                System.Diagnostics.Debug.WriteLine("Account not found");
                return allForecastChart;
            }
            allForecastChart = (IEnumerable<ForecastChart>?)await _forecastRepository.GetAllByIdAsync(accountid);

            return allForecastChart;
        }

        public async Task<IEnumerable<ForecastChartData>?> GetHouseHoldForcastData(Guid forecastcartid)
        {
            var forecastChartToBeFound = await _forecastRepository.GetByIdAsync(forecastcartid);
            IEnumerable<ForecastChartData> allForecastChartData = null;

            //Check if account exist
            if (forecastChartToBeFound == null)
            {
                System.Diagnostics.Debug.WriteLine("Forecast Chart not found");
                return allForecastChartData;
            }
            allForecastChartData = await _forecastDataRepository.GetAllByIdAsync(forecastcartid);

            return allForecastChartData;
        }


    }
}
