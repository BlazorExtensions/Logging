using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Blazor.Extensions.Logging
{
    internal class BrowserConsoleLogger<T> : BrowserConsoleLogger, ILogger<T>
    {
        public BrowserConsoleLogger (IJSRuntime jsRuntime, NavigationManager navigationManager) : base (jsRuntime, navigationManager, typeof (T).FullName, null)
        {
        }

        public BrowserConsoleLogger (IJSRuntime jsRuntime, NavigationManager navigationManager, Func<string, LogLevel, bool> filter) : base (jsRuntime, navigationManager, typeof (T).FullName, filter)
        {

        }
    }

    internal class BrowserConsoleLogger : ILogger, IAsyncDisposable
    {
        private const string LoggerFunctionName = "log";
        private const string ScriptName = "_content/Blazor.Extensions.Logging/BrowserConsoleLogger.js";

        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private IJSObjectReference module;
        private Func<string, LogLevel, bool> filter;

        public BrowserConsoleLogger (IJSRuntime jsRuntime, NavigationManager navigationManager, string name, Func<string, LogLevel, bool> filter)
        {
            this.filter = filter ?? ((category, logLevel) => true);
            this.Name = name ??
                throw new ArgumentNullException (nameof (name));

            this.moduleTask = new (() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", navigationManager.ToAbsoluteUri(ScriptName)).AsTask());
        }

        public async void Log<TState> (LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (this.module == null)
            {
                this.module = await moduleTask.Value;
            }

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

            await this.module.InvokeAsync<object> (LoggerFunctionName, message);
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

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;

                await module.DisposeAsync();
            }
        }
    }
}
