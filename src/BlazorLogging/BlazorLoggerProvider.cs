using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace BlazorLogging
{
    internal class BlazorLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, BlazorLogger> _loggers = new ConcurrentDictionary<string, BlazorLogger>();

        public ILogger CreateLogger(string categoryName) => this._loggers.GetOrAdd(categoryName, this.CreateLoggerImplementation);

        public void Dispose() => this._loggers.Clear();

        private BlazorLogger CreateLoggerImplementation(string name) => new BlazorLogger();
    }
}
