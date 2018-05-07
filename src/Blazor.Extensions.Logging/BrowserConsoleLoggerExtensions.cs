using Microsoft.Extensions.Logging;
using System;

namespace Blazor.Extensions.Logging
{
    /// <summary>
    /// ILogger extension methods for common scenarios.
    /// </summary>
    public static partial class BrowserConsoleLoggerExtensions
    {
        private static readonly Func<object, Exception, string> MessageFormatter = MessageFormatterFunc;

        /// <summary>
        /// Writes an object as a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.LogDebug(person)</example>
        public static void LogDebug<T>(this ILogger logger, T data) where T : class
        {
            logger.Log(LogLevel.Debug, data);
        }

        /// <summary>
        /// Writes an object as a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.LogDebug(exception, person)</example>
        public static void LogDebug<T>(this ILogger logger, Exception exception, T data) where T : class
        {
            logger.Log(LogLevel.Debug, exception, data);
        }

        /// <summary>
        /// Writes an object as a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.LogTrace(person)</example>
        public static void LogTrace<T>(this ILogger logger, T data) where T : class
        {
            logger.Log(LogLevel.Trace, data);
        }

        /// <summary>
        /// Writes an object as a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.LogTrace(exception, person)</example>
        public static void LogTrace<T>(this ILogger logger, Exception exception, T data) where T : class
        {
            logger.Log(LogLevel.Trace, exception, data);
        }


        /// <summary>
        /// Writes an object as an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.LogInformation(exception, person)</example>
        public static void LogInformation<T>(this ILogger logger, Exception exception, T data) where T : class
        {
            logger.Log(LogLevel.Information, exception, data);
        }

        /// <summary>
        /// Writes an object as an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.LogInformation(exception, person)</example>
        public static void LogInformation<T>(this ILogger logger, T data) where T : class
        {
            logger.Log(LogLevel.Information, data);
        }

        /// <summary>
        /// Writes an object as a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.LogWarning(exception, person)</example>
        public static void LogWarning<T>(this ILogger logger, T data) where T : class
        {
            logger.Log(LogLevel.Warning, data);
        }

        /// <summary>
        /// Writes an object as a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.LogWarning(exception, person)</example>
        public static void LogWarning<T>(this ILogger logger, Exception exception, T data) where T : class
        {
            logger.Log(LogLevel.Warning, exception, data);
        }

        /// <summary>
        /// Writes an object as a error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.LogError(exception, person)</example>
        public static void LogError<T>(this ILogger logger, T data) where T : class
        {
            logger.Log(LogLevel.Error, data);
        }

        /// <summary>
        /// Writes an object as a error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.LogError(exception, person)</example>
        public static void LogError<T>(this ILogger logger, Exception exception, T data) where T : class
        {
            logger.Log(LogLevel.Error, exception, data);
        }

        /// <summary>
        /// Writes an object as a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.LogCritical(exception, person)</example>
        public static void LogCritical<T>(this ILogger logger, T data) where T : class
        {
            logger.Log(LogLevel.Critical, data);
        }

        /// <summary>
        /// Writes an object as a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.LogCritical(exception, person)</example>
        public static void LogCritical<T>(this ILogger logger, Exception exception, T data) where T : class
        {
            logger.Log(LogLevel.Critical, exception, data);
        }

        /// <summary>
        /// Writes an object as a log message at the specified log level.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.Log(LogLevel.Information, exception, person)</example>
        public static void Log<T>(this ILogger logger, LogLevel logLevel, T data) where T : class
        {
            logger.Log(logLevel, null, data);
        }

        /// <summary>
        /// Writes an object as a log message at the specified log level.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="data">The object to log.</code></param>
        /// <example>logger.Log(LogLevel.Information, exception, person)</example>
        public static void Log<T>(this ILogger logger, LogLevel logLevel, Exception exception, T data) where T : class
        {
            Func<object, Exception, string> formatter = MessageFormatterFunc;

            logger.Log(logLevel, 0, new FormattedLogObject(logLevel, data, exception), exception, formatter);
        }

        private static string MessageFormatterFunc(object state, Exception error) => state.ToString();
    }
}
