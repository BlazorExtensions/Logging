Blazor.registerFunction('BlazorLogging.Trace', function (message) {
  return console.trace(JSON.parse(Blazor.platform.toJavaScriptString(message)));
});

Blazor.registerFunction('BlazorLogging.Debug', function (message) {
  return console.debug(JSON.parse(Blazor.platform.toJavaScriptString(message)));
});

Blazor.registerFunction('BlazorLogging.Info', function (message) {
  return console.info(JSON.parse(Blazor.platform.toJavaScriptString(message)));
});

Blazor.registerFunction('BlazorLogging.Warn', function (message) {
  return console.warn(JSON.parse(Blazor.platform.toJavaScriptString(message)));
});

Blazor.registerFunction('BlazorLogging.Error', function (message) {
  return console.error(JSON.parse(Blazor.platform.toJavaScriptString(message)));
});

Blazor.registerFunction('BlazorLogging.Log', function (message) {
  return console.log(JSON.parse(Blazor.platform.toJavaScriptString(message)));
});
