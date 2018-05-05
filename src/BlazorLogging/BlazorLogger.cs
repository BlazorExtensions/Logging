using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Browser.Interop;
using Microsoft.Extensions.Logging;
using System;

namespace BlazorLogging
{
    internal class BlazorLogger : ILogger
    {
        private const string PREFIX = "BlazorLogging";

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    RegisteredFunction.InvokeUnmarshalled<object>($"{PREFIX}.Trace", JsonUtil.Serialize(state));
                    break;
                case LogLevel.Debug:
                    RegisteredFunction.InvokeUnmarshalled<object>($"{PREFIX}.Debug", JsonUtil.Serialize(state));
                    break;
                case LogLevel.Information:
                    RegisteredFunction.InvokeUnmarshalled<object>($"{PREFIX}.Info", JsonUtil.Serialize(state));
                    break;
                case LogLevel.Warning:
                    RegisteredFunction.InvokeUnmarshalled<object>($"{PREFIX}.Warn", JsonUtil.Serialize(state));
                    break;
                case LogLevel.Critical:
                case LogLevel.Error:
                    RegisteredFunction.InvokeUnmarshalled<object>($"{PREFIX}.Error", JsonUtil.Serialize(state));
                    if (exception != null) RegisteredFunction.InvokeUnmarshalled<object>($"{PREFIX}.Error", JsonUtil.Serialize(exception));
                    break;
                default:
                    break;
            }
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
