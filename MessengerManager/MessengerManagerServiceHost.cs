using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Telegram.Bot;

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