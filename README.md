[![Build status](https://dotnet-ci.visualstudio.com/DotnetCI/_apis/build/status/Blazor-Extensions-Logging-CI?branch=master)](https://dotnet-ci.visualstudio.com/DotnetCI/_build/latest?definitionId=10&branch=master)
[![Package Version](https://img.shields.io/nuget/v/Blazor.Extensions.Logging.svg)](https://www.nuget.org/packages/Blazor.Extensions.Logging)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Blazor.Extensions.Logging.svg)](https://www.nuget.org/packages/Blazor.Extensions.Logging)
[![License](https://img.shields.io/github/license/BlazorExtensions/Logging.svg)](https://github.com/BlazorExtensions/Logging/blob/master/LICENSE)

# Blazor Extensions

Blazor Extensions is a set of packages with the goal of adding useful features to [Blazor](https://blazor.net).

# Blazor Extensions Logging

This package is an implementation for the [Microsoft Extensions Logging](https://github.com/aspnet/Logging) abstraction to support
using the ```ILogger``` interface in your Blazor code.

When the component is configured, all the log statements will appear in the browser's developer tools console.

# Features

## Content to log

The logger supports the same string formatting that MEL provides, together with named parameter replacement in the message.

Additionaly, you're able to log an object in the browser console. You can expand members and hierachies to see what's contained within.

If you want to log an enumerable list of objects, then the browser side component will display it by calling ```console.table```.

## Filtering

The implementation supports the ```ILoggerFactory``` based filtering configuration that is supplied by the Microsoft Extension Logging abstraction.

To keep it lightweight [Microsoft Extensions Configuration](https://github.com/aspnet/Configuration) based configuration is not supported, the logger can be only configured in code.

## Log levels

The logger supports the [LogLevels](https://github.com/aspnet/Logging/blob/dev/src/Microsoft.Extensions.Logging.Abstractions/LogLevel.cs) defined in MEL.

Some of the log levels are not available as distinct methods in the browser's developer tool, so the browser side component does some [mapping](https://github.com/BlazorExtensions/Logging/blob/master/src/Blazor.Extensions.Logging.JS/src/Initialize.ts#L35).

# Sample configuration

## Setup

The following snippet shows how to setup the browser console logger by registering it for dependency injection in the ```Startup.cs``` of the application.

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddLogging(builder => builder
        .AddBrowserConsole() // Add Blazor.Extensions.Logging.BrowserConsoleLogger
        .SetMinimumLevel(LogLevel.Trace)
    );
}
```

## Usage

The following snippet shows how to consume the logger in a Blazor component.

```c#
@inject ILogger<Index> logger

@functions {
  protected override async Task OnInitAsync()
  {
      logger.LogDebug("MyCompoent init");
  }
}
```

If you want to consume it outside of a ```cshtml``` based component, then you can use the ```Inject``` attribute to inject it into the class.

```c#
[Inject]
protected ILogger<MyClass> logger;

public void LogSomething()
{
  logger.LogDebug("Inside LogSomething");
}
```

# Contributions and feedback

Please feel free to use the component, open issues, fix bugs or provide feedback.

# Contributors

The following people are the maintainers of the Blazor Extensions projects:

- [Attila Hajdrik](https://github.com/attilah)
- [Gutemberg Ribiero](https://github.com/galvesribeiro)
