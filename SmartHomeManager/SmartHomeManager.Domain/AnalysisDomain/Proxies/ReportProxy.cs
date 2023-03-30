using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;
using SmartHomeManager.Domain.Common.Exceptions;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Services;

namespace SmartHomeManager.Domain.AnalysisDomain.Proxies
{
    public class ReportProxy : IReport
    {
        private readonly IAccountInfoService _accountService;
        private readonly IDeviceInformationService _deviceService;
        private readonly IReport _reportService;

        public ReportProxy(IReport reportService, IAccountRepository accountRepository, IDeviceRepository deviceRepository) 
        { 
            _reportService = reportService;
            _accountService = new AccountService(accountRepository);
            _deviceService = new MockDeviceService(deviceRepository);
        }

        public async Task<PdfFile> GetDeviceReport(Guid deviceId, int lastMonths)
        {

            // Check whether last months was 1, 3, or 6.
            if (lastMonths != 1 && lastMonths != 3 && lastMonths != 6)
            {
                throw new ArgumentException("Last months must be 1, 3 or 6.");
            }

            // Check if device exists...
            // Get device
            Device? device = await _deviceService.GetDeviceByIdAsync(deviceId);

            if (device == null)
            {
                throw new DeviceNotFoundException();
            }

            return await _reportService.GetDeviceReport(deviceId, lastMonths);
        }

        public async Task<PdfFile> GetHouseholdReport(Guid accountId, int lastMonths)
        {

            // Check whether last months was 1, 3, or 6.
            if (lastMonths != 1 && lastMonths != 3 && lastMonths != 6)
            {
                throw new ArgumentException("Last months must be 1, 3 or 6.");
            }

            // Check whether account is found
            Account? account = await _accountService.GetAccountByAccountId(accountId);

            if (account == null)
            {
                throw new AccountNotFoundException();
            }

            return await _reportService.GetHouseholdReport(accountId, lastMonths);
        }

        public async Task<IEnumerable<Device>?> GetDevicesByGUID(Guid accountId)
        {
            // Check whether account is found
            Account? account = await _accountService.GetAccountByAccountId(accountId);

            if (account == null)
            {
                throw new AccountNotFoundException();
            }
            
            return await _reportService.GetDevicesByGUID(accountId);
        }
    }
}
