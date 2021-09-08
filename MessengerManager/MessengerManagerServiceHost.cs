using System;
using System.Linq;
using System.Threading.Tasks;
using MessengerManager.Core.Configurations.Telegram;
using MessengerManager.Core.Handlers.TelegramHandlers;
using MessengerManager.Domain.Interfaces;
using MessengerManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MessengerManager
{
    public class MessengerManagerServiceHost
    {
        private IConfiguration _configuration;

        public IServiceProvider ServiceProvider { get; private set; }

        public MessengerManagerServiceHost(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Start()
        {
            var serviceCollection = new ServiceCollection();

            AddLogging(serviceCollection);
            AddDbContext(serviceCollection);
            AddServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            
            await ConfigureDbContext(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            
            await ConfigureHandlers();
        }

        public virtual void AddLogging(IServiceCollection serviceCollection)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("APP", "OZON-CONNECTOR")
                .WriteTo.Console()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("IdentityServer4", Serilog.Events.LogEventLevel.Error)
                .CreateLogger();
            serviceCollection.AddLogging(x => x.AddSerilog());
        }

        public virtual void AddDbContext(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<MessengerManagerDbContext>(opt =>
            {
                opt.UseNpgsql(_configuration["POSTGRESQL"]);
            });
        }
        
        public virtual async Task ConfigureDbContext(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork>(x => x.GetRequiredService<MessengerManagerDbContext>());
                
            var dbContext = ServiceProvider.GetRequiredService<MessengerManagerDbContext>();
            await dbContext.Database.MigrateAsync();
            
        }

        public async Task ConfigureHandlers()
        {
            var telegramBotClient = ServiceProvider.GetRequiredService<TelegramBotClient>();
            var telegramMessageHandler = ServiceProvider.GetRequiredService<TelegramMessageHandler>();

            var tasks = new[]
            {
                BaseTelegramHandler.StartHandler(telegramBotClient, UpdateType.Message,
                    telegramMessageHandler.UpdateHandler, telegramMessageHandler.ErrorHandler)
            };

            await Task.WhenAll(tasks);
        }
        public virtual void AddServices(IServiceCollection serviceCollection)
        {
            AddTelegramHandlers(serviceCollection);
        }

        private void AddTelegramHandlers(IServiceCollection serviceCollection)
        {
            var telegramConfiguration = _configuration.GetSection(TelegramConfiguration.ConfigName)
                .Get<TelegramConfiguration>();

            serviceCollection.AddOptions<TelegramConfiguration>(TelegramConfiguration.ConfigName);

            var telegramClient = new TelegramBotClient(telegramConfiguration.Token);

            serviceCollection.AddSingleton<ITelegramBotClient>(telegramClient);

            serviceCollection.AddSingleton<TelegramMessageHandler>();
        }
    }
}