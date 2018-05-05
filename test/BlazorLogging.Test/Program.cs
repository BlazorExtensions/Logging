using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorLogging.Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                // Add BlazorLogging
                services.AddLogging(builder => builder.AddBlazorLogger());
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
