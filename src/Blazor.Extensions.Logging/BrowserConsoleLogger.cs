using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;

namespace Blazor.Extensions.Logging
{
    internal class BrowserConsoleLogger : ILogger
    {
#if !DESKTOP_BUILD
        private const string LoggerFunctionName = "BlazorExtensions.Logging.BrowserConsoleLogger.Log";
#endif

        private Func<string, LogLevel, bool> filter;
        private IJSRuntime jsRuntime;

        public BrowserConsoleLogger(IJSRuntime jsRuntime, string name, Func<string, LogLevel, bool> filter)
        {
            this.jsRuntime = jsRuntime;
            this.filter = filter ?? ((category, logLevel) => true);
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            var message = formatter(state, exception);

            if (!(state is FormattedLogObject))
            {
                var internalFormatter = new FormattedLogObject(logLevel, message, exception);

                message = internalFormatter.ToString();
            }

#if !DESKTOP_BUILD
            this.jsRuntime.InvokeAsync<object>(LoggerFunctionName, message);
#else
            Console.WriteLine(message);
#endif
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel == LogLevel.None)
            {
                return false;
            }

            return this.Filter(this.Name, logLevel);
        }

        public Func<string, LogLevel, bool> Filter
        {
            get => this.filter;
            set
            {
                this.filter = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public string Name { get; }

        public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;

        private class NullScope : IDisposable
        {
            public static NullScope Instance { get; } = new NullScope();

            private NullScope()
            {
            }

            public void Dispose()
            {
            }
        }

    }
}
