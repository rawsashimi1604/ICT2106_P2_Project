using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Builders;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.AnalysisDomain.Interfaces
{
    public interface IPdfBuilder
    {
        public IPdfBuilder addDeviceDetails(Device device);
        public IPdfBuilder addDeviceLogTotalUsage(double totalUsage);
        public IPdfBuilder addDeviceEnergyEfficiency(EnergyEfficiency energyEfficiency);

        public IPdfBuilder addHouseholdDetails(Device device);

        public IPdfBuilder addHouseholdHeader(Account account);

        public IPdfBuilder addTotalHouseUsage(double householdUsage);

        public IPdfBuilder addGeneratedTime();

        public IPdfBuilder Date(DateTime start, DateTime end);

        public IPdfBuilder addMonthlyStats(
            int lastMonths,
            List<String> allMonthYearStrings,
            List<double> allEnergyCost,
            List<double> allEnergyUsage
            );

        public IPdfBuilder addTotalUsageCost(double overallUsage, double overallCost);

        public IPdfBuilder addDeviceParagraph();

        public IPdfBuilder addForecastReport(IEnumerable<ForecastChartData> forecast);

        public IPdfBuilder addHouseholdOverall(double usage, double cost);

        public IPdfBuilder addEnergyEfficiency(IEnumerable<EnergyEfficiency> energyEfficiency);

        public IPdfBuilder addEnergyHeader();

        public IPdfBuilder addDeviceEnergyHeader();

        public IPdfBuilder addAccountDetails(Account account);

        public byte[] Build();
    }
}
