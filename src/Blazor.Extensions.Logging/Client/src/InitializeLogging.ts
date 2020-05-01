import { BrowserConsoleLogger } from './BrowserConsoleLogger';

namespace Logging {
  const blazorExtensions = 'BlazorExtensions';
  // define what this extension adds to the window object inside BlazorExtensions
  const extensionObject = {
    Logging: {
      BrowserConsoleLogger: new BrowserConsoleLogger(),
    },
  };

  export const initialize = () => {
    if (typeof window !== 'undefined' && !(window as any)[blazorExtensions]) {
      // when the library is loaded in a browser via a <script> element, make the
      // following APIs available in global scope for invocation from JS
      (window as any)[blazorExtensions] = {
        ...extensionObject,
      };
    } else {
      (window as any)[blazorExtensions] = {
        ...(window as any)[blazorExtensions],
        ...extensionObject,
      };
    }
  };
}

Logging.initialize();
