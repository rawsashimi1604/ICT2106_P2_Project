using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using SmartHomeManager.Domain.APIDomain.Entities;
using SmartHomeManager.Domain.EnergyProfileDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.DeviceEnergyLimit.Entities;

namespace SmartHomeManager.DataSource;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        Accounts = base.Set<Account>();
        DeviceConfigurationLookUps = base.Set<DeviceConfigurationLookUp>();
        DeviceConfigurations = base.Set<DeviceConfiguration>();
        DeviceCoordinates = base.Set<DeviceCoordinate>();
        DeviceLogs = base.Set<DeviceLog>();
        DeviceTypes = base.Set<DeviceType>();
        DeviceProfiles = base.Set<DeviceProfile>();
        Devices = base.Set<Device>();
        Notifications = base.Set<Notification>();
        Profiles = base.Set<Profile>();
        RoomCoordinates = base.Set<RoomCoordinate>();
        Rooms = base.Set<Room>();
        Rules = base.Set<Rule>();
        Histories = base.Set<History>();
        RuleHistories = base.Set<RuleHistory>();
        Scenarios = base.Set<Scenario>();
        Troubleshooters = base.Set<Troubleshooter>();
        HomeSecurities = base.Set<HomeSecurity>();
        HomeSecuritySettings = base.Set<HomeSecuritySetting>();
        HomeSecurityDeviceDefinitions = base.Set<HomeSecurityDeviceDefinition>();
        APIDatas = base.Set<APIData>();
        APIKeys = base.Set<APIKey>();
        APIValues = base.Set<APIValue>();
        EnergyProfiles = base.Set<EnergyProfile>();
        DeviceProducts = base.Set<DeviceProduct>();
        CarbonFootprints = base.Set<CarbonFootprint>();
        ForecastCharts = base.Set<ForecastChart>();
        ForecastChartsData = base.Set<ForecastChartData>();
        EnergyEfficiency = base.Set<EnergyEfficiency>();
        DeviceEnergyLimit = base.Set<DeviceEnergyLimit>();
    }

    public DbSet<Account> Accounts { get; }
    public DbSet<DeviceConfigurationLookUp> DeviceConfigurationLookUps { get; }
    public DbSet<DeviceConfiguration> DeviceConfigurations { get; }
    public DbSet<DeviceCoordinate> DeviceCoordinates { get; }
    public DbSet<DeviceLog> DeviceLogs { get; }
    public DbSet<DeviceType> DeviceTypes { get; }
    public DbSet<Device> Devices { get; }
    public DbSet<DeviceProfile> DeviceProfiles { get; }
    public DbSet<Notification> Notifications { get; }
    public DbSet<Profile> Profiles { get; }
    public DbSet<RoomCoordinate> RoomCoordinates { get; }
    public DbSet<Room> Rooms { get; }
    public DbSet<DeviceProduct> DeviceProducts { get; }
    public DbSet<Rule> Rules { get; }
    public DbSet<History> Histories { get; }
    public DbSet<RuleHistory> RuleHistories { get; }
    public DbSet<Scenario> Scenarios { get; }
    public DbSet<Troubleshooter> Troubleshooters { get; }
    public DbSet<HomeSecurity> HomeSecurities { get; }
    public DbSet<HomeSecuritySetting> HomeSecuritySettings { get; }
    public DbSet<HomeSecurityDeviceDefinition> HomeSecurityDeviceDefinitions { get; }
    public DbSet<APIData> APIDatas { get; }
    public DbSet<APIKey> APIKeys { get; }
    public DbSet<APIValue> APIValues { get; }
    public DbSet<EnergyProfile> EnergyProfiles { get; }
    public DbSet<CarbonFootprint> CarbonFootprints { get; }
    public DbSet<ForecastChart> ForecastCharts { get; }
    public DbSet<ForecastChartData> ForecastChartsData { get; }
    public DbSet<EnergyEfficiency> EnergyEfficiency { get; }
    public DbSet<DeviceEnergyLimit> DeviceEnergyLimit { get; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DeviceConfigurationLookUp>()
            .HasKey(c => new { c.ConfigurationKey, c.DeviceBrand, c.DeviceModel });
        modelBuilder.Entity<DeviceConfiguration>().HasKey(c => new { c.ConfigurationKey, c.DeviceId });

        // Remove on delete cascade, make RoomId nullable on Device table...
        modelBuilder.Entity<Room>()
                    .HasMany(room => room.Devices)
                    .WithOne(device => device.Room)
                    .HasForeignKey(device => device.RoomId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<DeviceProfile>().HasKey(deviceProfile => new { deviceProfile.DeviceId, deviceProfile.ProfileId });
    }
}
