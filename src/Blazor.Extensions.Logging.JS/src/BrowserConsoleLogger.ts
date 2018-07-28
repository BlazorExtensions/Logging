import { LogObject, LogObjectType, LogLevel } from './LogObject';

interface IBrowserConsoleLogger {
  Log(logObjectValue: string): void;
}

export class BrowserConsoleLogger implements IBrowserConsoleLogger {
  Log(logObjectValue: string): void {
    const logObject: LogObject = JSON.parse(logObjectValue);
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

    logMethod(logObject.payload);

    if (logObject.exception) {
      logMethod("Exception: ", logObject.exception);
    }
  }
}
