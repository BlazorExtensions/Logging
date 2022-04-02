using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Components;

namespace Blazor.Extensions.Logging
{
    [ProviderAlias("BrowserConsole")]
    internal class BrowserConsoleLoggerProvider : ILoggerProvider
    {
        private static readonly Func<string, LogLevel, bool> TrueFilter = (cat, level) => true;

        private readonly Func<string, LogLevel, bool> filter;
        private readonly IJSRuntime runtime;
        private readonly NavigationManager navigation;
        private ConcurrentDictionary<string, BrowserConsoleLogger> loggers;

        public BrowserConsoleLoggerProvider(IJSRuntime runtime, NavigationManager navigation)
        {
            this.runtime = runtime;
            this.navigation = navigation;
        }

        public BrowserConsoleLoggerProvider(Func<string, LogLevel, bool> filter)
        {
            this.filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                throw new ArgumentNullException(nameof(categoryName));
            }

            if (this.loggers == null)
            {
                this.loggers = new ConcurrentDictionary<string, BrowserConsoleLogger>();
            }

            return this.loggers.GetOrAdd(categoryName, this.CreateLoggerImplementation);
        }

        public void Dispose() => this.loggers?.Clear();

        private BrowserConsoleLogger CreateLoggerImplementation(string name) => new BrowserConsoleLogger(this.runtime, navigation, name, this.GetFilter(name));

        private Func<string, LogLevel, bool> GetFilter(string name)
        {
            if (this.filter != null)
            {
                return this.filter;
            }

            return TrueFilter;
        }
    }
}
