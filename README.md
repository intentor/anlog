# ![Anlog](https://user-images.githubusercontent.com/5340818/38121535-4b734df8-33a6-11e8-98aa-e9b8d7234de0.png)

**(Yet) Another .NET Logger**

[![NuGet Version](http://img.shields.io/nuget/v/Anlog.svg?style=flat)](https://www.nuget.org/packages/Anlog/)

Fast and lightweight key/value pair logger for .NET Core projects.

## Contents
1. [Features](#features)
1. [Quick start](#quick-start)
1. [Why Anlog?](#why-anlog)
1. [Sinks](#sinks)
1. [Formatters](#formatters)
1. [Minimum log level](#minimum-log-level)
1. [Object logging](#object-logging)
1. [License](#license)

## Features

* Key/value pair approach ensures a standardization on logs.
* Writes to file or memory. 
* Easy to use through a static `Log` object.
* Fast when formatting and writing.

## Quick start

Install *Anlog* from the NuGet Gallery:

```
Install-Package Anlog
```

Create a console application and configure the logger:

```cs
using Anlog;
using Anlog.Factories;
using Anlog.Sinks.Console;

namespace QuickStart
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creates the logger.
            Log.Logger = new LoggerFactory()
                .MinimumLevel.Debug() // Minimum log level to write. In this case, Debug.
                .WriteTo.Console() // Write to the console.
                .CreateLogger();
            
            // Writes the log to console.
            Log.Append("key", "value").Info();
            
            // If possible, when the application ends, dispose the logger to ensure all logs are written.
            Log.Dispose();
        }
    }
}
```

Log produced: `2018-03-29 22:22:07.656 [INF] Program.Main:17 key=value`

## Why Anlog?

Keeping logs standardized for an entire application is no easy task. The idea of **Anlog** is to help make logs follow a default convention by using key/value pairs and default formats for different kinds of values.

Key/value pairs also offer an easy and compact way to analyze system logs, making it easier to read, find and standardize.

### History

**Anlog** started as an extension of the great (and widely used) [Serilog](https://serilog.net/) (that’s why it bears some resemblance!). However, due to difficulties of actually implementing it as planned, the project deviated from its original intention and began growing as a full-fledged component.

## Sinks

Sinks are objects that write the log to some output, like a file or memory.

### Console

Writes the log to the console.

```cs
Log.Logger = new LoggerFactory()
    .WriteTo.Console()
    .CreateLogger();
```

#### Settings

- *async*: True if write to the console should be asynchronous, otherwise false. Provides fast writing to console, however due to run in a separated thread, the last log(s) in case of a crash may not be written. The default is false.
- *theme: Output color theme. The default is none.</param>
- *minimumLevel*: Minimum log level. The default is the logger minimum level.

### SingleFile

Writes the log to a single file with unlimited size.

```cs
Log.Logger = new LoggerFactory()
    .WriteTo.SingleFile()
    .CreateLogger();
```

#### Settings

- *logFilePath*: log file path.
- *async*: True if write to the console should be asynchronous, otherwise false. Provides fast writing to console, however due to run in a separated thread, the last log(s) in case of a crash may not be written. The default is false.
- *encoding*: file encoding. The default is UTF8.
- *bufferSize*: buffer size to be used. The default is 4096.
- *minimumLevel*: Minimum log level. The default is the logger minimum level.

### InMemory

Writes the log to a memory buffer. 

It's recommended to use only in tests.

```cs
Log.Logger = new LoggerFactory()
    .WriteTo.InMemory()
    .CreateLogger();
```

To get the written logs, use `Log.GetSink()`:
```cs
var logs = Log.GetSink<InMemorySink>()?.GetLogs();
```
#### Settings

- *appendNewLine*: Indicates whether a new line should be appended at the end of each log. The default is true.
- *minimumLevel*: Minimum log level. The default is the logger minimum level.

## Formatters

Formats the key/value entries. All formatters include class, method name and line number of the log call.

### CompactKeyValue

Formats the key/value entries using a compact approach.

`2018-03-29 22:22:07.656 [INF] c=Program.Main:17 key=value`.

This is the default formatter and no configuration is required for using it.

#### Formats

- *Strings*: use the given string. E.g.: `key=text`
- *Numbers*: use `ToString(culture)`. E.g.: `key=24.11`
- *Dates*: use `ToString(dateTimeFormat)`. E.g.: `date=2018-03-25 23:00:00.000`
- *Enums*: use `ToString()`. E.g.: `key=Value`
- *Arrays* and *IEnumerable*: writes the items between `[]` separated by comma. E.g.: `key=[11.1,24.2,69.3,666.4]`
- *Classes* with fields and/or properties: writes the fields/properties as key/value pairs between `{}`. E.g.: `{field=24 property=666.11}`

#### Settings

- *culture*: culture to be used. The default is `CultureInfo.InvariantCulture`.
- *dateTimeFormat*: date/time log format. The default format is "yyyy-MM-dd HH:mm:ss.fff".

## Minimum log level

It's possible to set the minimum log level to write to sinks by using the `MinimumLevel` property in the `LoggerFactory`:

```cs
Log.Logger = new LoggerFactory()
    .MinimumLevel.Warn()
    .CreateLogger();
```

It's also possible to set the log level by using the `LogLevel` enumeration:

```cs
Log.Logger = new LoggerFactory()
    .MinimumLevel.Set(LogLevel.Info)
    .CreateLogger();
```

Each sink can override the logger minimum level. Please consult the [Sinks](#sinks) topic for settings of each sink.

### Available log levels

1. *Debug*: internal system logs that are usually intented for developers.
2. *Info*: general informative logs.
3. *Warn*: the system may not be behaving as expected.
4. *Error*: an unexpected issue occured.

The default minimum log level is *Info*.

## Object logging

### Ignoring fields/properties

If some field/property in an object needs to be ignored for logging, use the `LogIgnore` atribute:

```cs
public class Model
{
    [LogIgnore]
    public int IgnoreProperty { get; set; }
}
```

### Caching

Any object with a `DataContract` attribute will be cached for logging during initialization:

```cs
[DataContract]
public class Model
{
    ...
}
```

## License

Licensed under the [The MIT License (MIT)](http://opensource.org/licenses/MIT). Please see [LICENSE](https://raw.githubusercontent.com/intentor/anlog/master/LICENSE) for more information.
