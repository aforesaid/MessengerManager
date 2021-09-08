using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

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

            ConfigureDbContext(serviceCollection);
            
            
            ConfigureHandlers(serviceCollection);
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
            
        }

        public virtual void AddServices(IServiceCollection serviceCollection)
        {
            
        }

        public virtual void ConfigureDbContext(IServiceCollection serviceCollection)
        {
            
        }

        public virtual void ConfigureHandlers(IServiceCollection serviceCollection)
        {
            
        }
    }
}