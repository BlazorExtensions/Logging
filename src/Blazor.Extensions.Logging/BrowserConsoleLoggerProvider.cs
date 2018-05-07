using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;

namespace Blazor.Extensions.Logging
{
    [ProviderAlias("BrowserConsole")]
    internal class BrowserConsoleLoggerProvider : ILoggerProvider
    {
        private static readonly Func<string, LogLevel, bool> TrueFilter = (cat, level) => true;

        private readonly Func<string, LogLevel, bool> filter;

        private ConcurrentDictionary<string, BrowserConsoleLogger> loggers;

        public BrowserConsoleLoggerProvider()
        {
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

        private BrowserConsoleLogger CreateLoggerImplementation(string name) => new BrowserConsoleLogger(name, GetFilter(name));

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
