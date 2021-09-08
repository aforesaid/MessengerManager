using System;
using System.Threading.Tasks;
using MessengerManager.Core.Configurations.Telegram;
using MessengerManager.Core.Handlers.TelegramHandlers;
using MessengerManager.Domain.Entities;
using MessengerManager.Domain.Interfaces;
using MessengerManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MessengerManager
{
    public class MessengerManagerServiceHost
    {
        private readonly IConfiguration _configuration;

        private IServiceProvider ServiceProvider { get; set; }

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
                .Enrich.WithProperty("APP", "MessengerManager")
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

        public virtual void AddRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IGenericRepository<ChatThreadEntity>, EfGenericRepository<ChatThreadEntity>>();
        }
        
        public virtual async Task ConfigureDbContext(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork>(x => x.GetRequiredService<MessengerManagerDbContext>());
                
            var dbContext = ServiceProvider.GetRequiredService<MessengerManagerDbContext>();
            await dbContext.Database.MigrateAsync();
        }

        public async Task ConfigureHandlers()
        {
            var telegramBotClient = ServiceProvider.GetRequiredService<ITelegramBotClient>();
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
            AddRepositories(serviceCollection);
            
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