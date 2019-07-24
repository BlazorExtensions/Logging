#if !DESKTOP_BUILD
using Microsoft.JSInterop;
#endif
using Microsoft.Extensions.Logging;
#if DESKTOP_BUILD
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endif
using System;
using System.Collections;

namespace Blazor.Extensions.Logging
{
    internal class FormattedLogObject
    {
        private readonly LogLevel logLevel;
        private readonly object data;
        private readonly Exception exception;
        private readonly string category;

        // public FormattedLogObject(LogLevel logLevel, object data, Exception exception)
        // {
        //     this.logLevel = logLevel;
        //     this.data = data;
        //     this.exception = exception;
        // }

        public FormattedLogObject(string category, LogLevel logLevel, object data, Exception exception)
        {
            this.category = category;
            this.logLevel = logLevel;
            this.data = data;
            this.exception = exception;
        }

        public override string ToString()
        {
            if (this.data == null)
            {
                return string.Empty;
            }

            var logObject = default(LogObject);

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
                var isDataEnumerable = this.IsDataEnumerable(this.data);

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
                logObject.Exception = this.exception.ToString();
            }

#if !DESKTOP_BUILD
            return Newtonsoft.Json.JsonConvert.SerializeObject(logObject);
#else
            return JsonConvert.SerializeObject(logObject, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Converters = new[] { new StringEnumConverter(true) }
            });
#endif
        }

        private bool IsDataEnumerable(object data)
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
