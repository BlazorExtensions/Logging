using Blazor.Extensions.Logging;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace Blazor.Extensions.Logging.Test
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Add Blazor.Extensions.Logging.BrowserConsoleLogger
            builder.Services.AddLogging(builder2 => builder2
                .AddBrowserConsole() // Add Blazor.Extensions.Logging.BrowserConsoleLogger
                .SetMinimumLevel(LogLevel.Trace)
            );

            builder.Services.AddBaseAddressHttpClient();

            builder.RootComponents.Add<App>("app");

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}
