using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blazor.Extensions.Logging
{
    public class FormattedLogObject
    {
        private static readonly JsonSerializerOptions jsonOptions;

        private readonly LogLevel logLevel;
        private readonly object data;
        private readonly Exception exception;
        private readonly string category;

        static FormattedLogObject()
        {
            jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
#if DEBUG
                WriteIndented = true
#else
                WriteIndented = false
#endif
            };

            jsonOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        }

        public FormattedLogObject (string category, LogLevel logLevel, object data, Exception exception)
        {
            this.category = category;
            this.logLevel = logLevel;
            this.data = data;
            this.exception = exception;
        }

        public override string ToString ()
        {
            if (this.data == null)
            {
                return string.Empty;
            }

            var logObject = default (LogObject);

            if (this.data is string stringData)
            {
                logObject = new LogObject
                {
                    Category = this.category,
                    LogLevel = logLevel,
                    Type = LogObjectType.String,
                    Payload = stringData,
                };
            }
            else
            {
                var isDataEnumerable = this.IsDataEnumerable (this.data);

                logObject = new LogObject
                {
                    Category = this.category,
                    LogLevel = logLevel,
                    Type = isDataEnumerable ? LogObjectType.Table : LogObjectType.Object,
                    Payload = data
                };
            }

            if (this.exception != null)
            {
                logObject.Exception = this.exception.ToString ();
            }

            return JsonSerializer.Serialize(logObject, jsonOptions);
        }

        private bool IsDataEnumerable (object data)
        {
            if (data == null || data is string)
            {
                return false;
            }

            if (data as IEnumerable != null)
            {
                return true;
            }

            return false;
        }
    }
}
