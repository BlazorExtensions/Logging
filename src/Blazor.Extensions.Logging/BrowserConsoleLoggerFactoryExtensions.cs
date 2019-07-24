using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Blazor.Extensions.Logging
{
    public static class BrowserConsoleLoggerFactoryExtensions
    {
        /// <summary>
        /// Adds a logger that target the browser's console output
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        /// </summary>
        public static ILoggingBuilder AddBrowserConsole(this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, BrowserConsoleLoggerProvider>());

            // HACK: Override the hardcoded ILogger<> injected by Blazor
            builder.Services.Add(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(BrowserConsoleLogger<>)));

            return builder;
        }

        /// <summary>
        /// Adds a browser console logger that is enabled for <see cref="LogLevel"/>.Information or higher.
        /// </summary>
        /// <param name="factory">The <see cref="ILoggerFactory"/> to use.</param>
        public static ILoggerFactory AddBrowserConsole(this ILoggerFactory factory)
        {
            factory.AddBrowserConsole(LogLevel.Information);

            return factory;
        }

        /// <summary>
        /// Adds a browser console logger that is enabled for <see cref="LogLevel"/>s of minLevel or higher.
        /// </summary>
        /// <param name="factory">The <see cref="ILoggerFactory"/> to use.</param>
        /// <param name="minLevel">The minimum <see cref="LogLevel"/> to be logged</param>
        public static ILoggerFactory AddBrowserConsole(this ILoggerFactory factory, LogLevel minLevel)
        {
            factory.AddBrowserConsole((category, logLevel) => logLevel >= minLevel);

            return factory;
        }

        /// <summary>
        /// Adds a browser console logger that is enabled as defined by the filter function.
        /// </summary>
        /// <param name="factory">The <see cref="ILoggerFactory"/> to use.</param>
        /// <param name="filter">The category filter to apply to logs.</param>
        public static ILoggerFactory AddBrowserConsole(this ILoggerFactory factory, Func<string, LogLevel, bool> filter)
        {
            factory.AddProvider(new BrowserConsoleLoggerProvider(filter));

            return factory;
        }
    }
}
