import { BrowserConsoleLogger } from './BrowserConsoleLogger';

namespace Logging {
  const blazorExtensions: string = 'BlazorExtensions';
  // define what this extension adds to the window object inside BlazorExtensions
  const extensionObject = {
    Logging: {
      BrowserConsoleLogger: new BrowserConsoleLogger()
    }
  };

  export function initialize(): void {
    if (typeof window !== 'undefined' && !window[blazorExtensions]) {
      // when the library is loaded in a browser via a <script> element, make the
      // following APIs available in global scope for invocation from JS
      window[blazorExtensions] = {
        ...extensionObject
      };
    } else {
      window[blazorExtensions] = {
        ...window[blazorExtensions],
        ...extensionObject
      };
    }
  }
}

Logging.initialize();
