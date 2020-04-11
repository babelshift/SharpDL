using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SharpDL;
using SharpDL.Shared;

namespace Example0_Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = GetServiceProvider();
            var game = serviceProvider.GetService<IGame>();
            game.Run();
        }

        private static ServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSharpGame<MainGame>()
            .AddLogging(config => {
                config.AddConsole();
            })
            .Configure<LoggerFilterOptions>(options => {
                options.AddFilter<ConsoleLoggerProvider>(null, LogLevel.Trace);
            });
        }
    }
}
