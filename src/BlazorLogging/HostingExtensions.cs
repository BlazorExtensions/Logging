using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace BlazorLogging
{
    public static class HostingExtensions
    {
        /// <summary>
        /// Adds a logger that target the browser's console output
        /// </summary>
        public static ILoggingBuilder AddBlazorLogger(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, BlazorLoggerProvider>());
            return loggingBuilder;
        }
    }
}
