using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blazor.Extensions.Logging.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Blazor.Extensions.Logging.BrowserConsoleLogger
            services.AddLogging(builder => builder
                .AddBrowserConsole()
                .SetMinimumLevel(LogLevel.Trace)
            );
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
