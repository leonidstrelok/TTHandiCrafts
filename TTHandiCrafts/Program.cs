using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TTHandiCrafts.Data;

namespace TTHandiCrafts
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            await MigrateData(scope);
            // await MigrateDatabases(scope);
            await IdentityDataSeeder(scope);
            await host.RunAsync();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddEnvironmentVariables();

                    if (!context.HostingEnvironment.IsProduction())
                    {
                        builder.AddJsonFile($"identityconfig.{context.HostingEnvironment.EnvironmentName}.json",
                                optional: true)
                            .AddJsonFile($"emailConfig.{context.HostingEnvironment.EnvironmentName}.json",
                                optional: true)
                            .AddJsonFile($"serilogconfig.{context.HostingEnvironment.EnvironmentName}.json",
                                optional: true);
                    }
                    else
                    {
                        LoadProductionConfigs(builder);
                    }
                })
                .ConfigureServices((builder, services) =>
                {
                    services.AddScoped<IdentityDataSeeder>();
                    services.AddScoped<DataSeeder>();
                    services.AddScoped<MigrateData>();
                })
                .UseSerilog((context, configuration) =>
                {
                    configuration.Enrich.FromLogContext().ReadFrom.Configuration(context.Configuration);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("https://*:6655");
                });

        /// <summary>
        /// Загрузка файлов конфигурации для докера.
        /// </summary>
        /// <param name="builder"></param>
        private static void LoadProductionConfigs(IConfigurationBuilder builder)
        {
            var files = Directory.GetFiles("/config", "*.json", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                builder.AddJsonFile(file);
            }
        }


        /// <summary>
        /// Создание системного пользователя и добавление системных ролей
        /// </summary>
        /// <param name="scope"></param>
        private static async Task IdentityDataSeeder(IServiceScope scope)
        {
            IdentityDataSeeder identityDataSeeder = scope.ServiceProvider.GetRequiredService<IdentityDataSeeder>();
            await identityDataSeeder.SeedData();
        }

        /// <summary>
        /// Миграция всех данных при запуске приложения
        /// </summary>
        /// <param name="scope"></param>
        // private static async Task MigrateDatabases(IServiceScope scope)
        // {
        //     var databaseMigrator = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        //     await databaseMigrator.MigrationData();
        // }

        /// <summary>
        /// Миграция всех данных при запуске приложения
        /// </summary>
        /// <param name="scope"></param>
        private static async Task MigrateData(IServiceScope scope)
        {
            var databaseMigrator = scope.ServiceProvider.GetRequiredService<MigrateData>();
            await databaseMigrator.MigrationData();
        }
    }
}