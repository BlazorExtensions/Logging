using Microsoft.Extensions.Logging;

namespace Blazor.Extensions.Logging
{
    internal class LogObject
    {
        public LogLevel LogLevel { get; set; }
        public LogObjectType Type { get; set; }
        public object Payload { get; set; }
        public string Exception { get; set; }
    }
}
