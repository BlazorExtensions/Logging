type LogObject = {
  category: string;
  logLevel: LogLevel;
  type: LogObjectType;
  payload: unknown;
  exception: string;
}

// enum coming from Microsoft.Extensions.Logging
enum LogLevel {
  Trace = 'trace',
  Debug = 'debug',
  Information = 'information',
  Warning = 'warning',
  Error = 'error',
  Critical = 'critical',
  None = 'none'
}

enum LogObjectType {
  String = 'string',
  Object = 'object',
  Table = 'table'
}

export const log = (logObjectValue: string): void => {
  let logObject: LogObject;

  try {
    logObject = JSON.parse(logObjectValue);
  } catch (error) {
    throw new Error(`Error parsing JSON payload passed to BrowserConsoleLogger: ${error}`);
  }

  let logMethod: Function = console.log;

  // if we've a table, we'll print it as a table anyway, it is unlikely that the developer want to log errornous data as a table.
  if (logObject.type === LogObjectType.Table) {
    logMethod = console.table;
  } else {
    switch (logObject.logLevel) {
      case LogLevel.Trace:
        logMethod = console.trace;
        break;
      case LogLevel.Debug:
        logMethod = console.debug;
        break;
      case LogLevel.Warning:
        logMethod = console.warn;
        break;
      case LogLevel.Error:
      case LogLevel.Critical:
        logMethod = console.error;
        break;
    }
  }

  if (logObject.type == LogObjectType.Table) {
    logMethod(logObject.payload);
  } else {
    logMethod(`[${logObject.category}]`, logObject.payload);
  }

  if (logObject.exception) {
    logMethod(`[${logObject.category}] Exception:`, logObject.exception);
  }
};
