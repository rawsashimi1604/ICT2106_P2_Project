using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeManager.DataSource.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Timezone = table.Column<int>(type: "INTEGER", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    DevicesOnboarded = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "APIDatas",
                columns: table => new
                {
                    APIDataId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    Specification = table.Column<string>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIDatas", x => x.APIDataId);
                });

            migrationBuilder.CreateTable(
                name: "APIKeys",
                columns: table => new
                {
                    APIKeyType = table.Column<string>(type: "TEXT", nullable: false),
                    APILabelText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIKeys", x => x.APIKeyType);
                });

            migrationBuilder.CreateTable(
                name: "DeviceConfigurationLookUps",
                columns: table => new
                {
                    ConfigurationKey = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceBrand = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceModel = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigurationValue = table.Column<string>(type: "TEXT", nullable: false),
                    ValueMeaning = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceConfigurationLookUps", x => new { x.ConfigurationKey, x.DeviceBrand, x.DeviceModel });
                });

            migrationBuilder.CreateTable(
                name: "DeviceProducts",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    ProductBrand = table.Column<string>(type: "TEXT", nullable: false),
                    ProductModel = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceType = table.Column<string>(type: "TEXT", nullable: false),
                    ProductDescription = table.Column<string>(type: "TEXT", nullable: false),
                    ProductPrice = table.Column<double>(type: "REAL", nullable: false),
                    ProductQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceProducts", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                columns: table => new
                {
                    DeviceTypeName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.DeviceTypeName);
                });

            migrationBuilder.CreateTable(
                name: "ForecastChartsData",
                columns: table => new
                {
                    ForecastChartDataId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TimespanType = table.Column<int>(type: "INTEGER", nullable: false),
                    DateOfAnalysis = table.Column<string>(type: "TEXT", nullable: false),
                    Label = table.Column<string>(type: "TEXT", nullable: false),
                    WattsValue = table.Column<double>(type: "REAL", nullable: false),
                    PriceValue = table.Column<double>(type: "REAL", nullable: false),
                    Index = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastChartsData", x => x.ForecastChartDataId);
                });

            migrationBuilder.CreateTable(
                name: "HomeSecurities",
                columns: table => new
                {
                    HomeSecurityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SecurityModeState = table.Column<int>(type: "INTEGER", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSecurities", x => x.HomeSecurityId);
                });

            migrationBuilder.CreateTable(
                name: "HomeSecurityDeviceDefinitions",
                columns: table => new
                {
                    DeviceGroup = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigurationKey = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigurationOffValue = table.Column<int>(type: "INTEGER", nullable: false),
                    ConfigurationOnValue = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSecurityDeviceDefinitions", x => x.DeviceGroup);
                });

            migrationBuilder.CreateTable(
                name: "RuleHistories",
                columns: table => new
                {
                    RuleHistoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RuleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RuleIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    RuleName = table.Column<string>(type: "TEXT", nullable: false),
                    RuleStartTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RuleEndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RuleActionTrigger = table.Column<string>(type: "TEXT", nullable: true),
                    APIKey = table.Column<string>(type: "TEXT", nullable: true),
                    ApiValue = table.Column<string>(type: "TEXT", nullable: true),
                    ScenarioName = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceName = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceConfiguration = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleHistories", x => x.RuleHistoryId);
                });

            migrationBuilder.CreateTable(
                name: "CarbonFootprints",
                columns: table => new
                {
                    CarbonFootprintId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    HouseholdConsumption = table.Column<double>(type: "REAL", nullable: false),
                    NationalHouseholdConsumption = table.Column<double>(type: "REAL", nullable: false),
                    MonthOfAnalysis = table.Column<int>(type: "INTEGER", nullable: false),
                    YearOfAnalysis = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarbonFootprints", x => x.CarbonFootprintId);
                    table.ForeignKey(
                        name: "FK_CarbonFootprints_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergyProfiles",
                columns: table => new
                {
                    EnergyProfileId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ConfigurationValue = table.Column<int>(type: "INTEGER", nullable: false),
                    ConfigurationDesc = table.Column<string>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyProfiles", x => x.EnergyProfileId);
                    table.ForeignKey(
                        name: "FK_EnergyProfiles_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NotificationMessage = table.Column<string>(type: "TEXT", nullable: false),
                    SentTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileId);
                    table.ForeignKey(
                        name: "FK_Profiles_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APIValues",
                columns: table => new
                {
                    APIKeyType = table.Column<string>(type: "TEXT", nullable: false),
                    APIValues = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIValues", x => x.APIKeyType);
                    table.ForeignKey(
                        name: "FK_APIValues_APIKeys_APIKeyType",
                        column: x => x.APIKeyType,
                        principalTable: "APIKeys",
                        principalColumn: "APIKeyType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeSecuritySettings",
                columns: table => new
                {
                    HomeSecuritySettingId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeviceGroup = table.Column<string>(type: "TEXT", nullable: false),
                    HomeSecurityId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSecuritySettings", x => x.HomeSecuritySettingId);
                    table.ForeignKey(
                        name: "FK_HomeSecuritySettings_HomeSecurities_HomeSecurityId",
                        column: x => x.HomeSecurityId,
                        principalTable: "HomeSecurities",
                        principalColumn: "HomeSecurityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeSecuritySettings_HomeSecurityDeviceDefinitions_DeviceGroup",
                        column: x => x.DeviceGroup,
                        principalTable: "HomeSecurityDeviceDefinitions",
                        principalColumn: "DeviceGroup",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    HistoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeviceAdjustedConfiguration = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfileId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RuleHistoryId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_Histories_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Histories_RuleHistories_RuleHistoryId",
                        column: x => x.RuleHistoryId,
                        principalTable: "RuleHistories",
                        principalColumn: "RuleHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scenarios",
                columns: table => new
                {
                    ScenarioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ScenarioName = table.Column<string>(type: "TEXT", nullable: false),
                    ProfileId = table.Column<Guid>(type: "TEXT", nullable: false),
                    isActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenarios", x => x.ScenarioId);
                    table.ForeignKey(
                        name: "FK_Scenarios_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeviceName = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceBrand = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceModel = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceSerialNumber = table.Column<string>(type: "TEXT", nullable: false),
                    DevicePassword = table.Column<string>(type: "TEXT", nullable: true),
                    DeviceWatts = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomId = table.Column<Guid>(type: "TEXT", nullable: true),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Devices_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceTypes_DeviceTypeName",
                        column: x => x.DeviceTypeName,
                        principalTable: "DeviceTypes",
                        principalColumn: "DeviceTypeName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RoomCoordinates",
                columns: table => new
                {
                    RoomCoordinateId = table.Column<Guid>(type: "TEXT", nullable: false),
                    XCoordinate = table.Column<int>(type: "INTEGER", nullable: false),
                    YCoordinate = table.Column<int>(type: "INTEGER", nullable: false),
                    Width = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomCoordinates", x => x.RoomCoordinateId);
                    table.ForeignKey(
                        name: "FK_RoomCoordinates_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceConfigurations",
                columns: table => new
                {
                    ConfigurationKey = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeviceBrand = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceModel = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigurationValue = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceConfigurations", x => new { x.ConfigurationKey, x.DeviceId });
                    table.ForeignKey(
                        name: "FK_DeviceConfigurations_DeviceConfigurationLookUps_ConfigurationKey_DeviceBrand_DeviceModel",
                        columns: x => new { x.ConfigurationKey, x.DeviceBrand, x.DeviceModel },
                        principalTable: "DeviceConfigurationLookUps",
                        principalColumns: new[] { "ConfigurationKey", "DeviceBrand", "DeviceModel" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceConfigurations_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCoordinates",
                columns: table => new
                {
                    DeviceCoordinateId = table.Column<Guid>(type: "TEXT", nullable: false),
                    XCoordinate = table.Column<int>(type: "INTEGER", nullable: false),
                    YCoordinate = table.Column<int>(type: "INTEGER", nullable: false),
                    Width = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCoordinates", x => x.DeviceCoordinateId);
                    table.ForeignKey(
                        name: "FK_DeviceCoordinates_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceLogs",
                columns: table => new
                {
                    LogId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateLogged = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeviceEnergyUsage = table.Column<double>(type: "REAL", nullable: false),
                    DeviceActivity = table.Column<double>(type: "REAL", nullable: false),
                    DeviceState = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoomId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_DeviceLogs_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceLogs_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceProfiles",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProfileId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceProfiles", x => new { x.DeviceId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_DeviceProfiles_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceProfiles_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergyEfficiency",
                columns: table => new
                {
                    EnergyEfficiencyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EnergyEfficiencyIndex = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyEfficiency", x => x.EnergyEfficiencyId);
                    table.ForeignKey(
                        name: "FK_EnergyEfficiency_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    RuleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ScenarioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ConfigurationKey = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigurationValue = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionTrigger = table.Column<string>(type: "TEXT", nullable: true),
                    RuleName = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    APIKey = table.Column<string>(type: "TEXT", nullable: true),
                    ApiValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.RuleId);
                    table.ForeignKey(
                        name: "FK_Rules_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rules_Scenarios_ScenarioId",
                        column: x => x.ScenarioId,
                        principalTable: "Scenarios",
                        principalColumn: "ScenarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Troubleshooters",
                columns: table => new
                {
                    TroubleshooterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Recommendation = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Troubleshooters", x => x.TroubleshooterId);
                    table.ForeignKey(
                        name: "FK_Troubleshooters_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarbonFootprints_AccountId",
                table: "CarbonFootprints",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceConfigurations_ConfigurationKey_DeviceBrand_DeviceModel",
                table: "DeviceConfigurations",
                columns: new[] { "ConfigurationKey", "DeviceBrand", "DeviceModel" });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceConfigurations_DeviceId",
                table: "DeviceConfigurations",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCoordinates_DeviceId",
                table: "DeviceCoordinates",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceLogs_DeviceId",
                table: "DeviceLogs",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceLogs_RoomId",
                table: "DeviceLogs",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceProfiles_ProfileId",
                table: "DeviceProfiles",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_AccountId",
                table: "Devices",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceSerialNumber",
                table: "Devices",
                column: "DeviceSerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceTypeName",
                table: "Devices",
                column: "DeviceTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_RoomId",
                table: "Devices",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyEfficiency_DeviceId",
                table: "EnergyEfficiency",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyProfiles_AccountId",
                table: "EnergyProfiles",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_ProfileId",
                table: "Histories",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_RuleHistoryId",
                table: "Histories",
                column: "RuleHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSecuritySettings_DeviceGroup",
                table: "HomeSecuritySettings",
                column: "DeviceGroup");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSecuritySettings_HomeSecurityId",
                table: "HomeSecuritySettings",
                column: "HomeSecurityId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AccountId",
                table: "Notifications",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_AccountId",
                table: "Profiles",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomCoordinates_RoomId",
                table: "RoomCoordinates",
                column: "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AccountId",
                table: "Rooms",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_DeviceId",
                table: "Rules",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_ScenarioId",
                table: "Rules",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Scenarios_ProfileId",
                table: "Scenarios",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Troubleshooters_DeviceId",
                table: "Troubleshooters",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APIDatas");

            migrationBuilder.DropTable(
                name: "APIValues");

            migrationBuilder.DropTable(
                name: "CarbonFootprints");

            migrationBuilder.DropTable(
                name: "DeviceConfigurations");

            migrationBuilder.DropTable(
                name: "DeviceCoordinates");

            migrationBuilder.DropTable(
                name: "DeviceLogs");

            migrationBuilder.DropTable(
                name: "DeviceProducts");

            migrationBuilder.DropTable(
                name: "DeviceProfiles");

            migrationBuilder.DropTable(
                name: "EnergyEfficiency");

            migrationBuilder.DropTable(
                name: "EnergyProfiles");

            migrationBuilder.DropTable(
                name: "ForecastChartsData");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "HomeSecuritySettings");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RoomCoordinates");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Troubleshooters");

            migrationBuilder.DropTable(
                name: "APIKeys");

            migrationBuilder.DropTable(
                name: "DeviceConfigurationLookUps");

            migrationBuilder.DropTable(
                name: "RuleHistories");

            migrationBuilder.DropTable(
                name: "HomeSecurities");

            migrationBuilder.DropTable(
                name: "HomeSecurityDeviceDefinitions");

            migrationBuilder.DropTable(
                name: "Scenarios");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "DeviceTypes");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
