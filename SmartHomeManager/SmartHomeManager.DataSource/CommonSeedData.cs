using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource
{
    public class CommonSeedData
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            // Delete all existing database objects
            context.Accounts.RemoveRange(context.Accounts);
            await context.SaveChangesAsync();

            context.Profiles.RemoveRange(context.Profiles);

            context.Rooms.RemoveRange(context.Rooms);
            context.RoomCoordinates.RemoveRange(context.RoomCoordinates);
            context.DeviceTypes.RemoveRange(context.DeviceTypes);
            context.Devices.RemoveRange(context.Devices);
            context.DeviceCoordinates.RemoveRange(context.DeviceCoordinates);

            context.RuleHistories.RemoveRange(context.RuleHistories);
            context.DeviceProducts.RemoveRange(context.DeviceProducts);

            await context.SaveChangesAsync();

            // create objects
            var accounts = new List<Account>
            {
                new Account()
                {
                    AccountId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Email = "abc@xyz.com",
                    Username = "username",
                    Password = "xyZJhqMBMjJ1PGQ6ZiRKCu3RIVDZTKSthTZJ+52voFc=",
                    Address = "123 abc 456 888888",
                    Timezone = 8,
                    DevicesOnboarded = 3
                }
            };

            // add to repository and commit those changes
            await context.Accounts.AddRangeAsync(accounts);
            await context.SaveChangesAsync();

            // create objects
            var profiles = new List<Profile>
            {
                new Profile()
                {
                    ProfileId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    AccountId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Default Profile"
                }
            };

            // add to repository and commit those changes
            await context.Profiles.AddRangeAsync(profiles);
            await context.SaveChangesAsync();

            var rooms = new List<Room>
            {
                new()
                {
                    RoomId = Guid.NewGuid(),
                    Name = "Bedroom",
                    AccountId = accounts[0].AccountId
                },
                new()
                {
                    RoomId = Guid.NewGuid(),
                    Name = "Living room",
                    AccountId = accounts[0].AccountId
                },
            };

            var roomCoordinates = new List<RoomCoordinate>
            {
                new()
                {
                    XCoordinate = 0,
                    YCoordinate = 0,
                    Width = 5,
                    Height = 5,
                    RoomId = rooms[0].RoomId
                },
                new()
                {
                    XCoordinate = 6,
                    YCoordinate = 6,
                    Width = 5,
                    Height = 5,
                    RoomId = rooms[1].RoomId
                }
            };

            var deviceTypes = new List<DeviceType>
            {
                new DeviceType
                {
                    DeviceTypeName = "Light",
                },
                new DeviceType
                {
                    DeviceTypeName = "Aircon",
                },
                new DeviceType
                {
                    DeviceTypeName = "Fan",
                },
            };

            var devices = new List<Device>
            {
                new Device
                {
                    DeviceId = new("33333333-3333-3333-3333-333333333333"),
                    DeviceName = "Smart Fan",
                    DeviceBrand = "xiaomi",
                    DeviceModel = "smart fan",
                    DeviceWatts = 100,
                    DeviceTypeName = "Fan",
                    DeviceSerialNumber = "123",
                    AccountId = new ("11111111-1111-1111-1111-111111111111"),
                    RoomId = rooms[0].RoomId
                },
                new Device
                {
                    DeviceId = new("44444444-4444-4444-4444-444444444444"),
                    DeviceName = "Smart bulb",
                    DeviceBrand = "xiaomi",
                    DeviceModel = "smart bulb",
                    DeviceWatts = 100,
                    DeviceTypeName = "Light",
                    DeviceSerialNumber = "456",
                    AccountId = new ("11111111-1111-1111-1111-111111111111"),
                    RoomId = rooms[0].RoomId
                },
                new Device
                {
                    DeviceId = new("55555555-5555-5555-5555-555555555555"),
                    DeviceName = "Smart aircon",
                    DeviceBrand = "xiaomi",
                    DeviceModel = "smart aircon",
                    DeviceWatts = 100,
                    DeviceTypeName = "Aircon",
                    DeviceSerialNumber = "789",
                    AccountId = new ("11111111-1111-1111-1111-111111111111"),
                    RoomId = rooms[1].RoomId
                },

                 
            };

            // create objects
            var deviceProfiles = new List<DeviceProfile>
            {
                new DeviceProfile()
                {
                    DeviceId = devices[0].DeviceId,
                    ProfileId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                },
                new DeviceProfile()
                {
                    DeviceId = devices[1].DeviceId,
                    ProfileId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                },
                new DeviceProfile()
                {
                    DeviceId = devices[2].DeviceId,
                    ProfileId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                },
            };

            var deviceCoordinates = new List<DeviceCoordinate>
            {
                new()
                {
                    XCoordinate = 0,
                    YCoordinate = 0,
                    Width = 1,
                    Height = 1,
                    DeviceId = devices[0].DeviceId
                },
                new()
                {
                    XCoordinate = 1,
                    YCoordinate = 1,
                    Width = 1,
                    Height = 1,
                    DeviceId = devices[1].DeviceId
                },
                new()
                {
                    XCoordinate = 2,
                    YCoordinate = 2,
                    Width = 1,
                    Height = 1,
                    DeviceId = devices[2].DeviceId
                },
            };

            // add to repository and commit those changes
            await context.Rooms.AddRangeAsync(rooms);
            await context.SaveChangesAsync();

            await context.DeviceTypes.AddRangeAsync(deviceTypes);
            await context.SaveChangesAsync();
            
            await context.Devices.AddRangeAsync(devices);
            await context.SaveChangesAsync();
            
            await context.DeviceProfiles.AddRangeAsync(deviceProfiles);
            await context.SaveChangesAsync();

            await context.RoomCoordinates.AddRangeAsync(roomCoordinates);
            await context.SaveChangesAsync();

            await context.DeviceCoordinates.AddRangeAsync(deviceCoordinates);
            await context.SaveChangesAsync();

            var scenarios = new List<Scenario>
            {
                new Scenario
                {
                    ScenarioId = new("AC38AF14-9A57-4DF3-89F3-78F9CE9F4983"),
                    ScenarioName = "Default",
                    ProfileId = profiles[0].ProfileId
                }
            };

            await context.Scenarios.AddRangeAsync(scenarios);
            await context.SaveChangesAsync();

            var rules = new List<Rule>
            {
                new Rule
                {
                    RuleId = Guid.NewGuid(),
                    ScenarioId = scenarios[0].ScenarioId,
                    ConfigurationKey = "Speed",
                    ConfigurationValue = 1,
                    RuleName = "Fan Speed",
                    StartTime = Convert.ToDateTime("2023-02-04T07:21:26.934Z"),
                    EndTime = Convert.ToDateTime("2023-02-04T07:21:26.934Z"),
                    DeviceId = devices[0].DeviceId
                },
                new Rule
                {
                    RuleId = Guid.NewGuid(),
                    ScenarioId = scenarios[0].ScenarioId,
                    ConfigurationKey = "Oscillation",
                    ConfigurationValue = 1,
                    RuleName = "Fan Oscillation",
                    StartTime = Convert.ToDateTime("2023-02-04T07:21:26.934Z"),
                    EndTime = Convert.ToDateTime("2023-02-04T07:21:26.934Z"),
                    DeviceId = devices[0].DeviceId
                }
            };

            await context.Rules.AddRangeAsync(rules);
            await context.SaveChangesAsync();

            var ruleHistory = new List<RuleHistory>
            {
                new RuleHistory
                {            
                    RuleHistoryId = Guid.NewGuid(),
                    RuleId = rules[0].RuleId,
                    RuleIndex = 0,
                    RuleName = rules[0].RuleName,
                    RuleStartTime = rules[0].StartTime,
                    RuleEndTime = rules[0].EndTime,
                    RuleActionTrigger = rules[0].ActionTrigger,
                    ScenarioName = rules[0].Scenario.ScenarioName,
                    DeviceName = rules[0].Device.DeviceName,
                    DeviceConfiguration = string.Format("{0} triggering ...", rules[0].Device.DeviceName)
                }
            };

            await context.RuleHistories.AddRangeAsync(ruleHistory);
            await context.SaveChangesAsync();

            var history = new List<History>
            {
                new History
                {
                    Message = string.Format("{0} is triggered", rules[0].Device.DeviceName),
                    Timestamp = Convert.ToDateTime("2023-02-04T07:21:26.934Z"),
                    DeviceAdjustedConfiguration = 1,
                    ProfileId = profiles[0].ProfileId,
                    RuleHistoryId = ruleHistory[0].RuleHistoryId
                }
            };

            await context.Histories.AddRangeAsync(history);
            await context.SaveChangesAsync();

            Random rnd = new Random();
            var DeviceLogs = new List<DeviceLog>();
            for (int j = 13; j < 13 + 7; j++)
            {
                // create objects
                for (int i = 0; i < 23; i++)
                {
                    DeviceLogs.Add(new DeviceLog()
                    {
                        LogId = Guid.NewGuid(),
                        EndTime = DateTime.Parse($"2023-02-{j} {i + 1}:00:00.0000000"),
                        DateLogged = DateTime.Parse($"2023-02-{j} 00:00:00.0000000"),
                        DeviceEnergyUsage = rnd.Next(100, 1000),
                        DeviceActivity = 1,
                        DeviceState = false,
                        DeviceId = devices[0].DeviceId,
                        RoomId = rooms[0].RoomId,
                    });
                }
            }

            for (int j = 13; j < 13 + 7; j++)
            {
                // create objects
                for (int i = 0; i < 23; i++)
                {
                    DeviceLogs.Add(new DeviceLog()
                    {
                        LogId = Guid.NewGuid(),
                        EndTime = DateTime.Parse($"2023-02-{j} {i + 1}:00:00.0000000"),
                        DateLogged = DateTime.Parse($"2023-02-{j} 00:00:00.0000000"),
                        DeviceEnergyUsage = rnd.Next(100, 1000),
                        DeviceActivity = 1,
                        DeviceState = false,
                        DeviceId = devices[1].DeviceId,
                        RoomId = rooms[0].RoomId,
                    });
                }
            }

            for (int j = 13; j < 13 + 7; j++)
            {
                // create objects
                for (int i = 0; i < 23; i++)
                {
                    DeviceLogs.Add(new DeviceLog()
                    {
                        LogId = Guid.NewGuid(),
                        EndTime = DateTime.Parse($"2023-02-{j} {i + 1}:00:00.0000000"),
                        DateLogged = DateTime.Parse($"2023-02-{j} 00:00:00.0000000"),
                        DeviceEnergyUsage = rnd.Next(100, 1000),
                        DeviceActivity = 1,
                        DeviceState = false,
                        DeviceId = devices[2].DeviceId,
                        RoomId = rooms[1].RoomId,
                    });
                }
            }

            await context.DeviceLogs.AddRangeAsync(DeviceLogs);
            await context.SaveChangesAsync();

            int AmountOfNotificationsToBeSeeded = 20;
            for (int i = 0; i < AmountOfNotificationsToBeSeeded; i++)
            {
                Notification notification = new Notification
                {
                    AccountId = accounts[0].AccountId,
                    NotificationMessage = i + " - test notification",
                    SentTime = DateTime.Now,
                };

                // Add to database...
                await context.Notifications.AddRangeAsync(notification);
            }

            var deviceProducts = new List<DeviceProduct>
            {
                new()
                {
                    ProductName = "Xiao Mi Fan",
                    ProductBrand = "Xiao Mi",
                    ProductModel = "XM88",
                    ProductDescription = "Best Smart Fan",
                    DeviceType = "Fan",
                    ProductPrice = 250.80,
                    ProductQuantity = 100,
                    ProductImageUrl = "https://dynamic.zacdn.com/0D_IA2YBIJba0bZRGmDD2XKZDRc=/fit-in/346x500/filters:quality(95):fill(ffffff)/https://static-sg.zacdn.com/p/xiaomi-1178-9565502-1.jpg"
                }
            };

            await context.DeviceProducts.AddRangeAsync(deviceProducts);
            await context.SaveChangesAsync();
        }
    }
}
