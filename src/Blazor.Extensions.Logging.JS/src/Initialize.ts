import { LogObject, LogObjectType, LogLevel } from './LogObject';

function initialize() {
  "use strict";

  if (typeof window !== 'undefined' && !window['BlazorExtensions']) {
    // When the library is loaded in a browser via a <script> element, make the
    // following APIs available in global scope for invocation from JS
    window['BlazorExtensions'] = {
      Logging: {
        BrowserConsoleLogger: {
        }
      }
    };
  } else {
    window['BlazorExtensions'] = {
      ...window['BlazorExtensions'],
      Logging: {
        BrowserConsoleLogger: {
        }
      }
    };
  }

  window['BlazorExtensions']['Logging']['BrowserConsoleLogger']['Log'] = function (logObjectValue) {

    const logObject = JSON.parse(logObjectValue);
    var logMethod = console.log;

    // If we've a table, we'll print it as a table anyway, it is unlikely that the developer want to log errornous data as a table.
    if (logObject.type == LogObjectType.Table) {
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

initialize();
