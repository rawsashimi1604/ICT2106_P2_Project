using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SmartHomeManager.DataSource;
using SmartHomeManager.DataSource.HistoryDataSource;
using SmartHomeManager.DataSource.RuleDataSource;
using SmartHomeManager.DataSource.RuleHistoryDataSource;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Services;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // For allowing React to communicate with API
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });

            builder.Services.AddControllers();

            #region DEPENDENCY INJECTIONS
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IGenericRepository<History>, DataSource.HistoryDataSource.HistoryRepository>();
            builder.Services.AddScoped<IGenericRepository<RuleHistory>, RuleHistoryRepository>();
            builder.Services.AddScoped<IGenericRepository<Rule>, RuleRepository>();
            #endregion DEPENDENCY INJECTIONS

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHostedService<Director>();
            builder.Services.AddScoped<IScope, Scope>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

    }

    
}