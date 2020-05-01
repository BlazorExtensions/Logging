using System;

using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Blazor.Extensions.Logging
{
    internal class BrowserConsoleLogger<T> : BrowserConsoleLogger, ILogger<T>
    {
        public BrowserConsoleLogger (IJSRuntime runtime) : base (runtime, typeof (T).FullName, null)
        {

        }

        public BrowserConsoleLogger (IJSRuntime runtime, Func<string, LogLevel, bool> filter) : base (runtime, typeof (T).FullName, filter)
        {

        }
    }

    internal class BrowserConsoleLogger : ILogger
    {

        private const string LoggerFunctionName = "BlazorExtensions.Logging.BrowserConsoleLogger.Log";

        private readonly IJSRuntime runtime;
        private Func<string, LogLevel, bool> filter;

        public BrowserConsoleLogger (IJSRuntime runtime, string name, Func<string, LogLevel, bool> filter)
        {
            this.runtime = runtime;
            this.filter = filter ?? ((category, logLevel) => true);
            this.Name = name ??
                throw new ArgumentNullException (nameof (name));
        }

        public async void Log<TState> (LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled (logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException (nameof (formatter));
            }

            var message = formatter (state, exception);

            if (!(state is FormattedLogObject))
            {
                var internalFormatter = new FormattedLogObject (this.Name, logLevel, message, exception);

                message = internalFormatter.ToString ();
            }

            await this.runtime.InvokeAsync<object> (LoggerFunctionName, message);
        }

        public bool IsEnabled (LogLevel logLevel)
        {
            if (logLevel == LogLevel.None)
            {
                return false;
            }

            return this.Filter (this.Name, logLevel);
        }

        public Func<string, LogLevel, bool> Filter
        {
            get => this.filter;
            set
            {
                this.filter = value ??
                    throw new ArgumentNullException (nameof (value));
            }
        }

        public string Name { get; }

        public IDisposable BeginScope<TState> (TState state) => null;
    }
}
