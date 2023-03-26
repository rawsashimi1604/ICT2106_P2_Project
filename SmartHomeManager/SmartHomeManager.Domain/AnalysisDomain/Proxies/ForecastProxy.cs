using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;
using SmartHomeManager.Domain.Common.Exceptions;

namespace SmartHomeManager.Domain.AnalysisDomain.Proxies
{
    public class ForecastProxy : IForecast
    {

        private readonly IForecast _forecastService;
        private readonly AccountService _accountService;


        public ForecastProxy(
            IForecast forecastService,
            IAccountRepository accountRepository
        ) 
        {
            _forecastService = forecastService;
            _accountService = new AccountService(accountRepository);
        }

        public async Task<IEnumerable<ForecastChartData>> GetHouseHoldForecast(Guid accountId, int timespan)
        {

            // Check account exist...
            var accountToBeFound = await _accountService.GetAccountByAccountId(accountId);

            if (accountToBeFound == null)
            {
                System.Diagnostics.Debug.WriteLine("Account not found");
                throw new AccountNotFoundException();
            }


            // Check timespan between 0 and 2
            if (!(timespan >= 0 && timespan <= 2))
                throw new ArgumentException();

            return await _forecastService.GetHouseHoldForecast(accountId, timespan);
        }
    }
}
